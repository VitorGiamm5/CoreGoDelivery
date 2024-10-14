using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CoreGoDelivery.Infrastructure.Database
{
    public static class SetupDatabase
    {
        private const string NAME_SECTION_POSTRE = "Postgre";
        private const string NAME_MIGRATION_HISTORY = "__EFMigrationsHistory";
        private const string NAME_DATA_BASE = "dbgodelivery";
        private const int TIMEOUT_CONNECTIONDB = 1000;

        public static DbContextOptionsBuilder AddInfrastructure(this DbContextOptionsBuilder optionsBuilder, IConfiguration configuration)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(configuration.GetConnectionString(NAME_SECTION_POSTRE));
            dataSourceBuilder.EnableDynamicJson();
            dataSourceBuilder.ConnectionStringBuilder.IncludeErrorDetail = true;
            dataSourceBuilder.ConnectionStringBuilder.Timeout = TIMEOUT_CONNECTIONDB;

            var dataSouce = dataSourceBuilder.Build();
            optionsBuilder
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .UseNpgsql(dataSouce, b => b
                    .MigrationsHistoryTable(NAME_MIGRATION_HISTORY, NAME_DATA_BASE)
                    .EnableRetryOnFailure(2)
                    .MigrationsAssembly(typeof(GoDeliveryContext).Assembly.FullName)
                );

            return optionsBuilder;
        }
    }
}
