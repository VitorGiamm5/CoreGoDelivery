using CoreGoDelivery.Domain.Entities.GoDelivery.Motorcycle;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CoreGoDelivery.Infrastructure.Repositories.GoDelivery;

public class MotorcycleRepository : BaseRepository<MotorcycleEntity>, IMotorcycleRepository
{
    public MotorcycleRepository(ApplicationDbContext context) : base(context)
    {
        _context.Set<MotorcycleEntity>();
    }

    public async Task<List<MotorcycleEntity>?> List(string? plate)
    {
        if (string.IsNullOrEmpty(plate))
        {
            var entity = await _context.Set<MotorcycleEntity>()
                .Include(x => x.ModelMotorcycle)
                .Where(x => x.PlateNormalized == plate)
                .Take(100)
                .ToListAsync();

            return entity;
        }

        var resultWithParam = await _context.Set<MotorcycleEntity>()
            .Include(x => x.ModelMotorcycle)
            .Take(100)
            .ToListAsync();

        return resultWithParam;
    }

    public async Task<MotorcycleEntity?> GetOneByIdAsync(string id)
    {
        var entity = await _context.Set<MotorcycleEntity>()
            .Include(x => x.ModelMotorcycle)
            .FirstOrDefaultAsync(x => x.Id == id);

        return entity;
    }

    public async Task<bool> CheckIsUnicById(string id)
    {
        var entity = await _context.Set<MotorcycleEntity>()
            .FirstOrDefaultAsync(x => x.Id == id);

        return IsUnic(entity);
    }

    public async Task<bool> CheckIsUnicByPlateAsync(string plate)
    {
        var entity = await _context.Set<MotorcycleEntity>()
            .FirstOrDefaultAsync(x => x.PlateNormalized == plate);

        return IsUnic(entity);
    }

    public async Task<bool> Create(MotorcycleEntity data)
    {
        var entity = await _context
            .Set<MotorcycleEntity>()
            .AddAsync(data);

        await _context.SaveChangesAsync();

        return IsSuccessCreate(entity);
    }

    public async Task<bool> DeleteById(string id)
    {
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

    public async Task<bool> ChangePlateByIdAsync(string id, string plate)
    {
        var entity = await _context.Set<MotorcycleEntity>()
            .AsTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null)
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
