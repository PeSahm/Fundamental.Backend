# AI Coding Assistant Instructions for Fundamental.Backend

## Project Overview

This is a .NET backend service that processes Iranian financial statements from the CODAL API. It transforms versioned JSON reports into canonical domain entities for manufacturing companies' financial data (balance sheets, income statements, monthly activities, etc.).

**Architecture**: Clean Architecture with CQRS/MediatR pattern
- **Domain**: Core business logic, entities, value objects
- **Application**: CQRS commands/queries, DTOs, specifications
- **Infrastructure**: EF Core, external API clients, processors
- **Presentation**: ASP.NET Core Web API

## Key Patterns & Conventions

### CQRS Mediator Requests
- Use MediatR for all business operations
- Annotate requests with `[HandlerCode(HandlerCode.X)]`
- Error enums follow format: `{ClientCode}_{HandlerCode}_{ErrorSequence}`
  - ClientCode: From `Client.cs` (e.g., AdminWeb = 13)
  - HandlerCode: Last 3 digits of handler code
  - ErrorSequence: 101+ for first error
- Return `Response.Successful()` or error enum directly

### Versioned Processing
- JSON reports have multiple versions (V1-V5+)
- Use keyed DI services: `AddKeyedScopedCodalProcessor<>()`
- Factory pattern with metadata interfaces
- Store raw JSON + canonical normalized data

### Entity Design
- Rich domain entities with helper methods
- Value objects (FiscalYear, StatementMonth, IsoCurrency)
- Owned collections stored as JSON in DB
- Navigation properties with shadow FKs

### Testing
- Integration tests with real DB (TestFixture)
- Mock external APIs with WireMock
- Clean test data between runs
- Assert both raw JSON storage and canonical mapping

## Critical Workflows

### Building & Running
```bash
dotnet build
dotnet test  # Runs unit + integration tests
```

### Adding New Mediator Request
1. Create request record with `[HandlerCode(...)]`
2. Add unique HandlerCode to `HandlerCode.cs`
3. Create error enum with `[HandlerCode(...)]` and `[ErrorType(...)]`
4. Implement handler, validator
5. Register in DI

### Processing New Financial Statement Type
1. Analyze V5 JSON structure as canonical reference
2. Create domain entities with owned collections
3. Implement version-specific DTOs and processors
4. Add keyed service registrations
5. Create comprehensive integration tests

### Database Migrations
- Use EF Core migrations in `Migrations` project
- Run `dotnet ef migrations add <name>` from Migrations project
- Apply with `dotnet ef database update`

## Code Quality

### Analyzers
- StyleCop, SonarAnalyzer, custom rules
- Enforce via `Analyzers.ruleset` and `TestingAnalyzers.ruleset`

### Commit Messages
```text
#17 Clean room

Room was messy. Cleaned it up.
```
- <72 char title: imperative mood, no period
- Body: why, wrapped at 80 chars
- Check for sensitive data before commit

### Branching
- `feature/`: New features
- `bugfix/`: Bug fixes
- `hotfix/`: Production fixes
- Naming: lowercase, hyphens, no numbers

## External Dependencies

- **CODAL API**: Iranian stock exchange financial reports
- **Database**: PostgreSQL with JSONB for complex data
- **MediatR**: CQRS implementation
- **EF Core**: ORM with owned entities
- **FluentValidation**: Request validation
- **WireMock**: API mocking in tests

## Common Pitfalls

- Don't mix processing logic with mapping (use separate services)
- Always handle version differences in processors
- Use value objects, not primitives for domain concepts
- Test with real JSON samples, not mocks
- Validate EF configurations for owned collections</content>
<parameter name="filePath">.github/copilot-instructions.md