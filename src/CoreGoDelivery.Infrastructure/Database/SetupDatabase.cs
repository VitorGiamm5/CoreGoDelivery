using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CoreGoDelivery.Infrastructure.Database;

public static class SetupDatabase
{
    public static DbContextOptionsBuilder AddInfrastructure(this DbContextOptionsBuilder optionsBuilder, IConfiguration configuration)
    {
        // Define a string de conexão para ambiente local ou Docker
        var connectionString = configuration.GetConnectionString("postgres");
        if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
        {
            connectionString = "Server=postgres; Port=5432; Database=dbgodelivery; User ID=randandan; Password=randandan_XLR;";
        }

        // Cria o builder do DataSource e aplica configurações adicionais
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString)
        {
            ConnectionStringBuilder =
            {
                IncludeErrorDetail = true,
                Timeout = 100 // Timeout em segundos (ajuste de 1000 ms para 100 s)
            }
        };

        dataSourceBuilder.EnableDynamicJson();

        // Configurações finais no optionsBuilder
        optionsBuilder
            .EnableDetailedErrors()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .UseNpgsql(dataSourceBuilder.Build(), b => b
                .MigrationsHistoryTable("__EFMigrationsHistory", "dbgodelivery")
                .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
            );

        return optionsBuilder;
    }
}
