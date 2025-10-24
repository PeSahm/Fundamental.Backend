# Copilot Instructions for Fundamental.Backend

## Project Overview
- **Architecture:** Modular monorepo with layered structure: Application, BuildingBlock, Domain, Infrastructure, Presentation, and tests. Each layer is a separate project under `src/`.
- **Service Boundaries:** Each subfolder in `src/` is a distinct service or library. Communication is via direct .NET references, not HTTP or messaging.
- **Data Flow:** Domain logic is in `Domain/`, business logic in `Application/`, infrastructure (persistence, caching, etc.) in `Infrastructure/`, and API endpoints in `Presentation/Fundamental.WebApi`.

## Developer Workflows
- **Build:** Use the VS Code task `build` or run `dotnet build` from the repo root.
- **Test:** Unit tests are in `tests/UnitTests/`. Run with `dotnet test` from the repo root or target specific test projects.
- **Deploy:** See `.github/workflows/liara.yaml` for CI/CD. Deployment uses Liara CLI (`liara deploy ...`).

## Project-Specific Conventions
- **Error Handling:** Centralized in `src/BuildingBlock/ErrorHandling/`. Use `Result`, `Error`, and `IResponse` types for all service and API responses.
- **Dependency Injection:** Register services via extension methods in each layer, e.g., `ApplicationDependencyInjection.cs`.
- **DTOs & Models:** Domain models are in `Domain/`, API models in `Presentation/Fundamental.WebApi/Controllers`.
- **Configuration:** Shared config in `global.json`, `Directory.Build.props`, and per-project `appsettings.json`.
- **Testing:** Test projects mirror the structure of their source (e.g., `Application.UnitTests` for `Fundamental.Application`).

## Integration Points
- **External Services:** Deployment uses Liara (see workflow YAML). No other external integrations detected.
- **Cross-Component Communication:** Use .NET project references, not REST or messaging.

## Patterns & Examples
- **Error Handling:**
  ```csharp
  return Result.Success(data);
  return Result.Failure(Error.NotFound("message"));
  ```
- **Dependency Injection:**
  ```csharp
  services.AddApplication(); // See ApplicationDependencyInjection.cs
  ```
- **Testing:**
  ```shell
  dotnet test tests/UnitTests/Application.UnitTests
  ```

## Key Files & Directories
- `src/Application/Fundamental.Application/ApplicationDependencyInjection.cs` (DI setup)
- `src/BuildingBlock/ErrorHandling/Result.cs` (error/result pattern)
- `src/Presentation/Fundamental.WebApi/Program.cs` (API entrypoint)
- `.github/workflows/liara.yaml` (CI/CD)
- `tests/UnitTests/` (tests)

---

**Feedback requested:** If any conventions, workflows, or architectural details are unclear or missing, please specify so this guide can be improved.
