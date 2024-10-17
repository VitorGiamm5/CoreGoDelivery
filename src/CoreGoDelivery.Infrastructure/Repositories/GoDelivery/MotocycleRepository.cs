using CoreGoDelivery.Domain.Entities.GoDelivery.Motorcycle;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CoreGoDelivery.Infrastructure.Repositories.GoDelivery
{
    public class MotocycleRepository : BaseRepository<MotorcycleEntity>, IMotocycleRepository
    {
        public MotocycleRepository(AplicationDbContext context) : base(context)
        {
        }

        public async Task<List<MotorcycleEntity>?> List(string? plate)
        {
            if (!string.IsNullOrEmpty(plate))
            {
                var resultWithParam = await _context.Set<MotorcycleEntity>()
                    .Include(x => x.ModelMotorcycle)
                    .Where(x => x.PlateNormalized == plate)
                    .ToListAsync();

                return resultWithParam;
            }
            else
            {
                var result = await _context.Set<MotorcycleEntity>()
                    .Include(x => x.ModelMotorcycle)
                    .Take(100)
                    .ToListAsync();

                return result;
            }
        }

        public async Task<MotorcycleEntity?> GetOneByIdAsync(string id)
        {
            var result = await _context.Set<MotorcycleEntity>()
                .Include(x => x.ModelMotorcycle)
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<bool> CheckIsUnicById(string id)
        {
            var result = await _context.Set<MotorcycleEntity>()
                .FirstOrDefaultAsync(x => x.Id == id);

            return !Ok(result);
        }

        public async Task<bool> CheckIsUnicByPlateAsync(string plate)
        {
            var result = await _context.Set<MotorcycleEntity>()
                .FirstOrDefaultAsync(x => x.PlateNormalized == plate);

            return !Ok(result);
        }

        public async Task<bool> Create(MotorcycleEntity data)
        {
            var result = await _context
                .Set<MotorcycleEntity>()
                .AddAsync(data);

            await _context.SaveChangesAsync();

            return IsSuccessCreate(result);
        }

        public async Task<bool> DeleteById(string? id)
        {
            if (id == null)
            {
                return false;
            }

            var motorcycle = await GetOneByIdAsync(id);

            if (motorcycle != null)
            {
                _context.Set<MotorcycleEntity>()
                    .Remove(motorcycle);

                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> ChangePlateByIdAsync(string? id, string? plate)
        {
            var entity = await _context.Set<MotorcycleEntity>()
                .AsTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null || plate == null)
            {
                return false;
            }
            else
            {
                entity.PlateNormalized = plate;

                await _context.SaveChangesAsync();

                return true;
            }
        }
    }
}
