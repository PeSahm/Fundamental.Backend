# Backend Deployment Guide

Deployment requirements and flow for the Fundamental Backend (.NET 9).

## Docker Build

Multi-stage build producing two images:

| Image | Purpose | Base |
|-------|---------|------|
| `fundamental-backend` | ASP.NET Core 9 API | `mcr.microsoft.com/dotnet/aspnet:9.0-alpine` |
| `fundamental-migrations` | EF Core database migrations | `mcr.microsoft.com/dotnet/aspnet:9.0-alpine` |

**Runtime Details:**
- Non-root user: `appuser` (UID 1000)
- Port: 8080
- Timezone: Asia/Tehran
- Graceful shutdown: 30s timeout
- Health check: `curl -f http://localhost:8080/health/live`

## Required Secrets

### GitHub Secrets (CI/CD)
| Secret | Purpose |
|--------|---------|
| `REGISTRY_USERNAME` | Container registry auth (fundamental) |
| `REGISTRY_PASSWORD` | Container registry auth |
| `SENTRY_AUTH_TOKEN` | Sentry debug symbol uploads |
| `INFRA_REPO_TOKEN` | GitOps repository dispatch trigger |

### GitHub Variables
| Variable | Purpose |
|----------|---------|
| `SENTRY_ENABLED` | Enable Sentry integration (true/false) |
| `SENTRY_UPLOAD_SOURCEMAPS` | Upload debug symbols (true/false) |

### Kubernetes Secrets
| Secret Name | Namespace | Keys |
|-------------|-----------|------|
| `fundamental-backend-secrets` | fundamental-dev/prod | `jwt-secret`, `api-key` |
| `postgresql-credentials` | fundamental-dev/prod | `username`, `password`, `connection-string` |
| `redis-credentials` | fundamental-dev/prod | `password` |
| `sentry-credentials` | fundamental-dev/prod | `dsn`, `frontend-dsn`, `auth-token` |
| `registry-credentials` | fundamental-dev/prod | Docker config JSON |

## Health Check Endpoints

| Endpoint | Purpose |
|----------|---------|
| `/health/live` | Liveness probe (is the process running?) |
| `/health/ready` | Readiness probe (can it serve traffic?) |

## CI/CD Flow

```
Push to develop/main
        │
        ▼
Build & Test (.NET 9 on self-hosted runner)
  ├─ dotnet restore
  ├─ dotnet build -c Release
  └─ dotnet test
        │
        ▼
Build Docker Images
  ├─ Build fundamental-backend
  ├─ Build fundamental-migrations
  ├─ Push to registry.academind.ir
  └─ Upload Sentry debug symbols (optional)
        │
        ▼
Trigger GitOps
  └─ repository_dispatch to Fundamental.Infra
        │
        ▼
ArgoCD syncs deployment
```

### Image Tagging
| Branch | Tag Pattern | Latest Tag |
|--------|-------------|------------|
| develop | `dev-YYYYMMDD-SHORT_SHA` | `dev-latest` |
| main | `1.0.0-YYYYMMDD-SHORT_SHA` | `prod-latest` |

## Sentry Integration

- Package: `Sentry.Serilog` v5.16.1
- Org: `fundamental`
- Project: `fundamental-backend`
- URL: `https://sentry.academind.ir`
- Config: `appsettings.json` → `Serilog.Using` must include `Sentry.Serilog`

## Database Migrations

Migrations run as a Kubernetes Job before the backend starts. The migrator image contains the EF Core migration bundle.

```bash
# Check migration status
microk8s kubectl get jobs -n fundamental-dev -l app.kubernetes.io/component=migrator

# Re-run migrations
microk8s kubectl delete job -n fundamental-dev -l app.kubernetes.io/component=migrator
# ArgoCD recreates it automatically
```

## Known Issues

1. **Serilog.Sinks.Sentry vs Sentry.Serilog**: The correct assembly name in `appsettings.json` `Using` array is `Sentry.Serilog`, NOT `Serilog.Sinks.Sentry`.
2. **Self-hosted runner required**: CI/CD uses labels `self-hosted, Linux, X64, Iran` due to network restrictions.
3. **Sentry CLI**: Downloaded from GitHub releases (not sentry.io) due to network restrictions.
