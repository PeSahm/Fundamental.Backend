using System.Reflection;
using Coravel.Pro;
using ErrorHandling.AspNetCore;
using FluentValidation;
using Fundamental.Application;
using Fundamental.Infrastructure.Extensions;
using Fundamental.Infrastructure.Persistence;
using Fundamental.Infrastructure.Serialization;
using Fundamental.Web.Common.Extensions;
using Fundamental.WebApi.Extensions;
using Fundamental.WebApi.Middlewares;
using Fundamental.WebApi.Swagger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;
using Serilog.Events;

WebApplicationBuilder builder = WebApplication.CreateSlimBuilder(args);

// =============================================================================
// Sentry - Error Tracking & Performance Monitoring
// =============================================================================
// Sentry must be initialized as early as possible to capture all errors
// DSN is read from: 1) Sentry__Dsn env var, 2) Configuration Sentry:Dsn
string? sentryDsn = Environment.GetEnvironmentVariable("Sentry__Dsn")
                    ?? builder.Configuration["Sentry:Dsn"];

builder.WebHost.UseSentry(options =>
{
    // Set DSN explicitly from environment or configuration
    options.Dsn = sentryDsn ?? string.Empty;

    // Environment is set automatically from ASPNETCORE_ENVIRONMENT
    options.Environment = builder.Environment.EnvironmentName.ToLowerInvariant();

    // Enable performance monitoring
    options.TracesSampleRate = builder.Environment.IsProduction() ? 0.2 : 1.0;

    // Enable profiling (requires Sentry.Profiling package)
    options.ProfilesSampleRate = builder.Environment.IsProduction() ? 0.1 : 1.0;

    // Automatically capture unhandled exceptions
    options.SendDefaultPii = false; // GDPR compliance

    // Debug mode for development
    options.Debug = builder.Environment.IsDevelopment();

    // Add breadcrumbs for better debugging
    options.MaxBreadcrumbs = 50;

    // Auto session tracking
    options.AutoSessionTracking = true;

    // Attach stacktrace to all messages
    options.AttachStacktrace = true;

    // Release tracking (set via environment variable or CI/CD)
    options.Release = Environment.GetEnvironmentVariable("SENTRY_RELEASE")
                      ?? Assembly.GetExecutingAssembly().GetName().Version?.ToString()
                      ?? "unknown";
});

builder.WebHost.UseKestrelHttpsConfiguration();

builder.Services.AddControllers(options =>
    {
        options.AddDefaultResultConvention();
    })
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
        x.JsonSerializerOptions.Converters.Add(new SaleColumnIdConvertor());
        x.JsonSerializerOptions.Converters.Add(new StatementMonthJsonConverter());
        x.JsonSerializerOptions.Converters.Add(new FiscalYearJsonConverter());
        x.JsonSerializerOptions.Converters.Add(new CodalMoneyJsonConverter());
    })
    .ConfigureCustomApiBehaviorOptions();
builder.Services.Configure<ApiBehaviorOptions>(options
    => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddRedis(builder.Configuration).AddCustomHybridCache(builder.Configuration);
builder.Services.AddDbContexts(builder.Configuration)
    .AddInterceptors();
builder.Services.AddServices();
builder.AddOptions();
builder.Services.AddCustomHttpClient(builder.Configuration);
builder.Services.AddReadRepositories();
builder.Services.AddHostedServices();
builder.Services.AddCodalServices();
builder.Services.AddEventDispatcher();
builder.Configuration
    .AddEnvironmentVariables();
builder.Services.AddBuilders().AddDbRetryPolicy();
builder.Services.AddCap();

builder.Services.AddRazorPages().AddNewtonsoftJson();
builder.Services.AddCoravelPro(typeof(FundamentalDbContext));

builder.Services.AddHttpContextAccessor();
builder.Host.UseSerilog((context, serviceProvider, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
    configuration.ReadFrom.Services(serviceProvider);

    // Send Serilog events to Sentry (Warning and above)
    // Only configure Sentry sink if DSN is available
    string? serilogSentryDsn = Environment.GetEnvironmentVariable("Sentry__Dsn")
                               ?? context.Configuration["Sentry:Dsn"];

    if (!string.IsNullOrEmpty(serilogSentryDsn))
    {
        configuration.WriteTo.Sentry(o =>
        {
            o.Dsn = serilogSentryDsn;
            o.MinimumEventLevel = LogEventLevel.Error;
            o.MinimumBreadcrumbLevel = LogEventLevel.Warning;
        });
    }
});
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
});
builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IApiDescriptionProvider, CustomerApiDescriptionProvider>());
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomSwagger();
builder.Services.AddValidatorsFromAssemblies(new List<Assembly> { typeof(ApplicationDependencyInjection).Assembly });

builder.Services.AddCors(options =>
{
    string[]? origins = builder.Configuration.GetSection("CorsOrigins").Get<string[]>();
    options.AddDefaultPolicy(corsPolicyBuilder =>
    {
        if (origins != null)
        {
            corsPolicyBuilder.WithOrigins(origins)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetPreflightMaxAge(TimeSpan.FromHours(1))
                .SetIsOriginAllowedToAllowWildcardSubdomains();
        }
    });
});

// Health checks for Kubernetes probes
builder.Services.AddCustomHealthChecks(builder.Configuration);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
// Sentry middleware is added automatically by UseSentry()
app.UseCustomSwaggerUi();

app.UseHttpsRedirection();

// Sentry tracing for performance monitoring
app.UseSentryTracing();

app.UseRouting();
app.UseCors();
app.UseMiddleware<ErrorLoggingMiddleware>();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();
app.MapCustomHealthChecks();

if (app.Configuration.GetValue<bool>("JobEnabled"))
{
    app.UseCoravelPro();
}

await app.RunAsync();