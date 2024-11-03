using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CoreGoDelivery.Infrastructure.Database;

public static class SetupDatabase
{
    public static DbContextOptionsBuilder AddInfrastructure(this DbContextOptionsBuilder optionsBuilder, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("postgres");

        var isInDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

        if (isInDocker)
        {
            connectionString = "Server=postgres; Port=5432; Database=dbgodelivery; User ID=randandan; Password=randandan_XLR;";
        }

        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);

        dataSourceBuilder!.EnableDynamicJson();
        dataSourceBuilder!.ConnectionStringBuilder.IncludeErrorDetail = true;
        dataSourceBuilder!.ConnectionStringBuilder.Timeout = 1000;

        optionsBuilder
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .UseNpgsql(dataSourceBuilder.Build(), b => b
                .MigrationsHistoryTable("__EFMigrationsHistory", "dbgodelivery")
                .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                .EnableRetryOnFailure(3)
            );

        return optionsBuilder;
    }
}
