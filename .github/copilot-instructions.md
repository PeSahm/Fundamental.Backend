# Copilot Instructions for Fundamental.Backend# Copilot Instructions for Fundamental.Backend



## Project Overview## Project Overview

- **Architecture:** Modular monorepo with layered structure: Application, BuildingBlock, Domain, Infrastructure, Presentation, and tests. Each layer is a separate project under `src/`.- **Architecture:** Modular monorepo with layered structure: Application, BuildingBlock, Domain, Infrastructure, Presentation, and tests. Each layer is a separate project under `src/`.

- **Service Boundaries:** Each subfolder in `src/` is a distinct service or library. Communication is via direct .NET references, not HTTP or messaging.- **Service Boundaries:** Each subfolder in `src/` is a distinct service or library. Communication is via direct .NET references, not HTTP or messaging.

- **Data Flow:** Domain logic is in `Domain/`, business logic in `Application/`, infrastructure (persistence, caching, etc.) in `Infrastructure/`, and API endpoints in `Presentation/Fundamental.WebApi`.- **Data Flow:** Domain logic is in `Domain/`, business logic in `Application/`, infrastructure (persistence, caching, etc.) in `Infrastructure/`, and API endpoints in `Presentation/Fundamental.WebApi`.

- **CODAL Processing:** Multi-version processor pattern - external CODAL financial data comes in 5+ schema versions (V1-V5), each processed by dedicated processors that map to canonical entities. Processors use keyed DI to resolve version-specific mapping services.

## Developer Workflows

## Developer Workflows- **Build:** Use the VS Code task `build` or run `dotnet build` from the repo root.

- **Build:** Use the VS Code task `build` or run `dotnet build` from the repo root.- **Test:** Unit tests are in `tests/UnitTests/`. Run with `dotnet test` from the repo root or target specific test projects.

- **Test:** Unit tests are in `tests/UnitTests/`, integration tests in `tests/IntegrationTests/`. Run with `dotnet test` from the repo root or target specific test projects.- **Deploy:** See `.github/workflows/liara.yaml` for CI/CD. Deployment uses Liara CLI (`liara deploy ...`).

- **Migrations:** EF migrations are in `src/Infrastructure/Migrations/`. Create with `dotnet ef migrations add <Name> --project src/Infrastructure/Migrations`. Apply with `dotnet ef database update --project src/Infrastructure/Migrations`.

- **Deploy:** See `.github/workflows/liara.yaml` for CI/CD. Deployment uses Liara CLI (`liara deploy ...`).## Project-Specific Conventions

- **Error Handling:** Centralized in `src/BuildingBlock/ErrorHandling/`. Use `Result`, `Error`, and `IResponse` types for all service and API responses.

## Project-Specific Conventions- **Dependency Injection:** Register services via extension methods in each layer, e.g., `ApplicationDependencyInjection.cs`.

- **Error Handling:** Centralized in `src/BuildingBlock/ErrorHandling/`. Use `Result`, `Error`, and `IResponse` types for all service and API responses.- **DTOs & Models:** Domain models are in `Domain/`, API models in `Presentation/Fundamental.WebApi/Controllers`.

- **Dependency Injection:** - **Configuration:** Shared config in `global.json`, `Directory.Build.props`, and per-project `appsettings.json`.

  - Register services via extension methods in each layer (e.g., `ApplicationDependencyInjection.cs`)- **Testing:** Test projects mirror the structure of their source (e.g., `Application.UnitTests` for `Fundamental.Application`).

  - **CRITICAL:** Keyed service factories must match the scope of services they resolve. Factories resolving Scoped services must be Scoped, not Singleton.

  - Keyed services use metadata-based keys (e.g., `"MappingService-Production-MonthlyActivity-V5-NotSpecified"`) generated from DTO metadata implementing `ICodalMappingServiceMetadata`## Integration Points

- **DTOs & Models:** - **External Services:** Deployment uses Liara (see workflow YAML). No other external integrations detected.

  - Domain models are in `Domain/`, API models in `Presentation/Fundamental.WebApi/Controllers`- **Cross-Component Communication:** Use .NET project references, not REST or messaging.

  - External CODAL DTOs in `Application/Codals/Dto/MonthlyActivities/V{1-5}/` implement `ICodalMappingServiceMetadata` for self-describing metadata (ReportingType, LetterType, CodalVersion, LetterPart)

- **Configuration:** Shared config in `global.json`, `Directory.Build.props`, and per-project `appsettings.json`.## Patterns & Examples

- **Testing:** Test projects mirror the structure of their source (e.g., `Application.UnitTests` for `Fundamental.Application`).- **Error Handling:**

- **Database:**   ```csharp

  - PostgreSQL with EF Core using snake_case naming (via `NpgsqlSnakeCaseNameTranslator`)  return Result.Success(data);

  - DateTime properties for timestamps use `timestamp with time zone` and must be stored as UTC (call `.ToUniversalTime()`)  return Result.Failure(Error.NotFound("message"));

  - Entity configurations in `Infrastructure/Configuration/Fundamental/` inherit from `EntityTypeConfigurationBase<T>`  ```

- **Dependency Injection:**

## Integration Points  ```csharp

- **External Services:**   services.AddApplication(); // See ApplicationDependencyInjection.cs

  - CODAL API for Iranian financial disclosures (multiple schema versions)  ```

  - Deployment uses Liara (see workflow YAML)- **Testing:**

- **Cross-Component Communication:** Use .NET project references, not REST or messaging.  ```shell

  dotnet test tests/UnitTests/Application.UnitTests

## Patterns & Examples  ```

- **Error Handling:**

  ```csharp## Key Files & Directories

  return Result.Success(data);- `src/Application/Fundamental.Application/ApplicationDependencyInjection.cs` (DI setup)

  return Result.Failure(Error.NotFound("message"));- `src/BuildingBlock/ErrorHandling/Result.cs` (error/result pattern)

  ```- `src/Presentation/Fundamental.WebApi/Program.cs` (API entrypoint)

- **Keyed DI Registration (Mapping Services):**- `.github/workflows/liara.yaml` (CI/CD)

  ```csharp- `tests/UnitTests/` (tests)

  // Register factory (must be Scoped if resolving Scoped services)

  services.AddScoped<ICanonicalMappingServiceFactory, CanonicalMappingServiceFactory>();---

  

  // Register keyed mapping services**Feedback requested:** If any conventions, workflows, or architectural details are unclear or missing, please specify so this guide can be improved.

  services.AddKeyedScopedCanonicalMappingService<
      ICanonicalMappingService<CanonicalMonthlyActivity, CodalMonthlyActivityV5>,
      MonthlyActivityMappingServiceV5,
      CodalMonthlyActivityV5>();
  ```
- **Resolving Keyed Services (in Processors):**
  ```csharp
  ICanonicalMappingService<CanonicalMonthlyActivity, CodalMonthlyActivityV5> mappingService =
      mappingServiceFactory.GetMappingService<CanonicalMonthlyActivity, CodalMonthlyActivityV5>();
  ```
- **UTC DateTime Storage:**
  ```csharp
  canonical.PublishDate = statement.PublishDateMiladi.ToUniversalTime();
  ```
- **Testing:**
  ```shell
  dotnet test tests/UnitTests/Application.UnitTests
  dotnet test tests/IntegrationTests --filter "MonthlyActivity"
  ```

## Key Files & Directories
- `src/Application/Fundamental.Application/ApplicationDependencyInjection.cs` (DI setup)
- `src/Infrastructure/Extensions/Codals/Manufacturing/ServicesConfigurationExtensions.cs` (CODAL processor & mapping service registration)
- `src/Infrastructure/Services/Codals/Factories/` (Keyed service factory pattern for versioned processors)
- `src/Infrastructure/Services/Codals/Manufacturing/Processors/MonthlyActivities/` (V1-V5 processors showing version-specific processing)
- `src/BuildingBlock/ErrorHandling/Result.cs` (error/result pattern)
- `src/Infrastructure/Persistence/FundamentalDbContext.cs` (EF context)
- `src/Presentation/Fundamental.WebApi/Program.cs` (API entrypoint)
- `.github/workflows/liara.yaml` (CI/CD)

---

## Architecture Notes
- **Processor Pattern:** Each CODAL schema version has a dedicated processor (e.g., `MonthlyActivityV5Processor`) implementing `ICodalProcessor`. Processors are registered with keyed DI using static metadata properties (ReportingType, LetterType, CodalVersion, LetterPart).
- **Mapping Services:** Version-specific mapping services convert external DTOs to canonical domain entities. Factory pattern with keyed DI ensures correct mapper is resolved at runtime based on DTO metadata.
- **Canonical Entities:** All external data versions map to single canonical entity (`CanonicalMonthlyActivity`), enabling queries across versions without version-specific logic.
