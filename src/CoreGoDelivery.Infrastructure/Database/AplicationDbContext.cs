using Microsoft.EntityFrameworkCore;

namespace CoreGoDelivery.Infrastructure.Database
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("dbgodelivery");
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(builder);
        }
    }
}
