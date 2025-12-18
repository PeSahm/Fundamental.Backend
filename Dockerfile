# =============================================================================
# Fundamental Backend - Production Dockerfile
# =============================================================================
# Multi-stage build for ASP.NET Core 9.0 application
# Follows Kubernetes 2025 best practices:
# - Non-root user (UID 1000)
# - Minimal base image (Alpine)
# - Health check endpoint
# - Graceful shutdown support
# =============================================================================

# -----------------------------------------------------------------------------
# Stage 1: Build
# -----------------------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /src

# Copy solution and project files first (better layer caching)
COPY ["Fundamental.sln", "."]
COPY ["Directory.Build.props", "."]
COPY ["global.json", "."]

# Copy all project files
COPY ["src/Application/Fundamental.Application/Fundamental.Application.csproj", "src/Application/Fundamental.Application/"]
COPY ["src/BuildingBlock/ErrorHandling/ErrorHandling.csproj", "src/BuildingBlock/ErrorHandling/"]
COPY ["src/BuildingBlock/ErrorHandling.AspNetCore/ErrorHandling.AspNetCore.csproj", "src/BuildingBlock/ErrorHandling.AspNetCore/"]
COPY ["src/BuildingBlock/Fundamental.BuildingBlock/Fundamental.BuildingBlock.csproj", "src/BuildingBlock/Fundamental.BuildingBlock/"]
COPY ["src/Domain/Fundamental.Domain/Fundamental.Domain.csproj", "src/Domain/Fundamental.Domain/"]
COPY ["src/Infrastructure/Fundamental.Infrastructure/Fundamental.Infrastructure.csproj", "src/Infrastructure/Fundamental.Infrastructure/"]
COPY ["src/Infrastructure/Migrations/Migrations.csproj", "src/Infrastructure/Migrations/"]
COPY ["src/Presentation/Fundamental.WebApi/Fundamental.WebApi.csproj", "src/Presentation/Fundamental.WebApi/"]
COPY ["src/Presentation/Web.Common/Web.Common.csproj", "src/Presentation/Web.Common/"]

# Restore dependencies
RUN dotnet restore "src/Presentation/Fundamental.WebApi/Fundamental.WebApi.csproj"

# Copy everything else
COPY . .

# Build the application
# Note: Analyzers are skipped in Docker build (not needed for runtime)
# They are excluded via .dockerignore and disabled via build properties
WORKDIR "/src/src/Presentation/Fundamental.WebApi"
RUN dotnet build "Fundamental.WebApi.csproj" -c Release -o /app/build --no-restore \
    -p:RunAnalyzers=false \
    -p:RunAnalyzersDuringBuild=false \
    -p:EnableNETAnalyzers=false

# -----------------------------------------------------------------------------
# Stage 2: Publish
# -----------------------------------------------------------------------------
FROM build AS publish
RUN dotnet publish "Fundamental.WebApi.csproj" -c Release -o /app/publish \
    --no-restore \
    /p:UseAppHost=false \
    /p:PublishTrimmed=false

# -----------------------------------------------------------------------------
# Stage 3: Runtime
# -----------------------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final

# Install curl for health checks, ICU libraries for .NET globalization, and set timezone
RUN apk add --no-cache curl icu-libs tzdata \
    && cp /usr/share/zoneinfo/Asia/Tehran /etc/localtime \
    && echo "Asia/Tehran" > /etc/timezone \
    && apk del tzdata

# Create non-root user (UID 1000 to match Kubernetes securityContext)
RUN addgroup -g 3000 appgroup \
    && adduser -u 1000 -G appgroup -s /bin/sh -D appuser

WORKDIR /app

# Copy published files
COPY --from=publish /app/publish .

# Set ownership
RUN chown -R appuser:appgroup /app

# Switch to non-root user
USER appuser

# Expose port (ASP.NET Core 8+ default)
EXPOSE 8080

# Environment variables
ENV ASPNETCORE_URLS=http://+:8080 \
    ASPNETCORE_ENVIRONMENT=Production \
    DOTNET_RUNNING_IN_CONTAINER=true \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
    # Graceful shutdown
    DOTNET_SHUTDOWNTIMEOUTSECONDS=30

# Health check
HEALTHCHECK --interval=30s --timeout=10s --start-period=60s --retries=3 \
    CMD curl -f http://localhost:8080/health/live || exit 1

# Entry point
ENTRYPOINT ["dotnet", "Fundamental.WebApi.dll"]
