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

WebApplicationBuilder builder = WebApplication.CreateSlimBuilder(args);

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
WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCustomSwaggerUi();

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors();
app.UseMiddleware<ErrorLoggingMiddleware>();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

if (app.Configuration.GetValue<bool>("JobEnabled"))
{
    app.UseCoravelPro();
}

await app.RunAsync();