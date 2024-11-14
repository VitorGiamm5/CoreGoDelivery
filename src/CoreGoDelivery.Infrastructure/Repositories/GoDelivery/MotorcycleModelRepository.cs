using CoreGoDelivery.Domain.Entities.GoDelivery.MotorcycleModel;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CoreGoDelivery.Infrastructure.Repositories.GoDelivery;

public class MotorcycleModelRepository : BaseRepository<MotorcycleModelEntity>, IMotorcycleModelRepository
{
    public MotorcycleModelRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<MotorcycleModelEntity>> GetAll(string? name, string? id)
    {
        if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(id))
        {
            return await _context.Set<MotorcycleModelEntity>()
            .ToListAsync();
        }

        var result = await _context.Set<MotorcycleModelEntity>()
            .Where(x => x.Name == name || x.Id == id)
            .ToListAsync();

        return result;
    }

    public async Task<string> GetIdByModelName(string model)
    {
        var result = await _context.Set<MotorcycleModelEntity>()
            .FirstOrDefaultAsync(x => x.NormalizedName == model);

        return result?.Id ?? "";
    }
}
