using Fundamental.Infrastructure.Extensions;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContexts(builder.Configuration);
builder.AddServices();
builder.Host.ConfigureAppConfiguration(b => b
    .AddJsonFile("appsettings.Override.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables());

builder.Host.UseSerilog((context, serviceProvider, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(serviceProvider);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    string[] origins = builder.Configuration.GetSection("CorsOrigins").Get<string[]>();
    options.AddDefaultPolicy(corsPolicyBuilder =>
    {
        corsPolicyBuilder.WithOrigins(origins)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetPreflightMaxAge(TimeSpan.FromHours(1))
            .SetIsOriginAllowedToAllowWildcardSubdomains();
    });
});
WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
