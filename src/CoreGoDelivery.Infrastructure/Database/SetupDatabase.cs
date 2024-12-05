using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CoreGoDelivery.Infrastructure.Database;

public static class SetupDatabase
{
    public static DbContextOptionsBuilder AddInfrastructure(this DbContextOptionsBuilder optionsBuilder, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres");

        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString)
        {
            ConnectionStringBuilder =
            {
                IncludeErrorDetail = true,
                Timeout = 100
            }
        };

        dataSourceBuilder.EnableDynamicJson();

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
