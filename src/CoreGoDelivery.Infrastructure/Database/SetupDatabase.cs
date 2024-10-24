﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CoreGoDelivery.Infrastructure.Database
{
    public static class SetupDatabase
    {
        public static DbContextOptionsBuilder AddInfrastructure(this DbContextOptionsBuilder optionsBuilder, IConfiguration configuration)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(configuration.GetConnectionString("postgre"));

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
