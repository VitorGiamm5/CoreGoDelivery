using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CoreGoDelivery.Infrastructure.Database
{
    public class GoDeliveryContext : DbContext
    {
        public GoDeliveryContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("godeliverydb");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

    }
}
