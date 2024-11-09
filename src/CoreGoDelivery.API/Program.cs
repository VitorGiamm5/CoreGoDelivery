using CoreGoDelivery.Api.Conveters;
using CoreGoDelivery.Api.Swagger;
using CoreGoDelivery.Application;
using CoreGoDelivery.Infrastructure.Database.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.WriteIndented = true;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.DefaultBufferSize = 4096;

    options.JsonSerializerOptions.Converters.Add(new TrimStringJsonConverter());
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.Converters.Add(new CustomDateTimeConverter("s"));
});

builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = $"Core Motorcycle, Rental, Deliverier - {builder.Environment.EnvironmentName}",
        Version = "v1"
    });
    c.CustomSchemaIds(type => type.ToString());
    c.OperationFilter<DefaultValuesOperation>();
});

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", false, true)
    .AddEnvironmentVariables();

EnvironmentVariablesExtensions.AddEnvironmentVariables(builder.Configuration);

builder.Services.AddApplication(builder.Configuration);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5273);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    ExecutePendingMigration.Execute(builder.Services);
}

app.MapControllers().WithMetadata(new RouteAttribute("api/[controller]")); ;

try
{
    Log.Information("Starting application...");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Fail to start application...");
}
finally
{
    Log.CloseAndFlush();
}
