using CoreGoDelivery.Domain.Entities.GoDelivery.Motocycle;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CoreGoDelivery.Infrastructure.Repositories.GoDelivery
{
    public class MotocycleRepository : BaseRepository<MotocycleEntity>, IMotocycleRepository
    {
        public MotocycleRepository(AplicationDbContext context) : base(context)
        {
        }

        public async Task<List<MotocycleEntity>?> List(string? plate)
        {
            var result = await _context.Set<MotocycleEntity>()
                .Where(x => plate != null || plate != "" ? x.Id == plate : x.Id != null)
                .Take(100)
                .ToListAsync();

            return result;
        }

        public async Task<MotocycleEntity?> GetOneById(string id)
        {
            var result = await _context.Set<MotocycleEntity>()
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<bool> CheckIsUnicById(string id)
        {
            var result = await _context.Set<MotocycleEntity>()
                .FirstOrDefaultAsync(x => x.Id == id);

            return Ok(result);
        }

        public async Task<bool> CheckIsUnicByPlateId(string plate)
        {
            var result = await _context.Set<MotocycleEntity>()
                .FirstOrDefaultAsync(x => x.PlateIdNormalized == plate);

            return Ok(result);
        }

        public async Task<bool> Create(MotocycleEntity data)
        {
            var result = await _context
                .Set<MotocycleEntity>()
                .AddAsync(data);

            await _context.SaveChangesAsync();

            return IsSuccessCreate(result);
        }

        public Task<bool> DeleteById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
