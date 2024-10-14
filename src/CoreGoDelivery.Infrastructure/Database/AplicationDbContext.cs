using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CoreGoDelivery.Infrastructure.Database
{
    public class AplicationDbContext : DbContext
    {
        private const string DEFAULT_SCHEMA = "godeliverydb";

        public AplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema(DEFAULT_SCHEMA);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

    }
}
