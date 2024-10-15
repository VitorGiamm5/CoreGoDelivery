using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CoreGoDelivery.Infrastructure.Database
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("godeliverydb");
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(builder);
        }

        //public async Task<IEnumerable<IEntityEventManager>> SaveAsync()
        //{

        //}
    }
}
