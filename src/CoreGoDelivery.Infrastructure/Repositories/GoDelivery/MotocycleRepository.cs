using CoreGoDelivery.Domain.Entities.GoDelivery.Motocycle;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CoreGoDelivery.Infrastructure.Repositories.GoDelivery
{
    public class MotocycleRepository : BaseRepository<MotocycleEntity>, IMotocycleRepository
    {
        public MotocycleRepository(AplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> CheckIsUnicById(string id)
        {
            var result = await _context.Set<MotocycleEntity>()
                .FirstOrDefaultAsync(x => x.Id == id);

            return result == null;
        }

        public async Task<bool> CheckIsUnicByPlateId(string plate)
        {
            var result = await _context.Set<MotocycleEntity>()
                .FirstOrDefaultAsync(x => x.PlateIdNormalized == plate);

            return result == null;
        }

        public async Task<bool> Create(MotocycleEntity data)
        {
            var result = await _context
                .Set<MotocycleEntity>()
                .AddAsync(data);

            await _context.SaveChangesAsync();

            return WasSuccessStatus(result);
        }

        private static bool WasSuccessStatus(EntityEntry<MotocycleEntity> result)
        {
            return result.State == EntityState.Added || result.State == EntityState.Unchanged;
        }
    }
}
