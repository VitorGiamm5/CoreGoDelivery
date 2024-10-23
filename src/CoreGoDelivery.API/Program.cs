using CoreGoDelivery.Api.Conveters;
using CoreGoDelivery.Api.Swagger;
using CoreGoDelivery.Application;
using CoreGoDelivery.Application.RabbitMQ.NotificationMotorcycle.Consumer;
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
        Title = $"Sistema de Manutencao de Motos - {builder.Environment.EnvironmentName}",
        Version = "v1"
    });
    c.CustomSchemaIds(type => type.ToString());
    c.OperationFilter<DefaultValuesOperation>();
});

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

EnvironmentVariablesExtensions.AddEnvironmentVariables(builder.Configuration);

builder.Services.AddControllersWithViews();
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddApplication(builder.Configuration);

var app = builder.Build();

var consumer = app.Services.GetService<RabbitMqConsumer>();
#pragma warning disable CS4014
Task.Run(() => consumer!.ConsumeMessages());
#pragma warning restore CS4014


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
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
