using System.Text.Json.Serialization;
using CoreGoDelivery.Api.Conveters;
using CoreGoDelivery.Api.Swagger;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
//object value = builder.Services.AddInfrastructure(builder.Configuration);
// var logFilePath = builder.Configuration["Serilog:LogFilePath"]!;
// Log.Logger = new LoggerConfiguration()
//     .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
//     .CreateLogger();
builder.Host.UseSerilog();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.WriteIndented = true;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.Converters.Add(new CustomDateTimeConverter("s"));
});

builder.Services.AddHealthChecks();
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

var app = builder.Build();

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
