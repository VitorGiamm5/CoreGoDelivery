using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CoreGoDelivery.Infrastructure.Database
{
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

            Console.WriteLine($"Connection String (postgres): {connectionString}");

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);

            dataSourceBuilder!.EnableDynamicJson();
            dataSourceBuilder!.ConnectionStringBuilder.IncludeErrorDetail = true;
            dataSourceBuilder!.ConnectionStringBuilder.Timeout = 1000;

            var dataSouce = dataSourceBuilder.Build();
            optionsBuilder
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .UseNpgsql(dataSouce, b => b
                    .MigrationsHistoryTable("__EFMigrationsHistory", "dbgodelivery")
                    .EnableRetryOnFailure(2)
                    .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                );

            return optionsBuilder;
        }
    }
}
