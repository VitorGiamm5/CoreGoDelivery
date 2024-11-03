using CoreGoDelivery.Domain.Entities.GoDelivery.RentalPlan;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CoreGoDelivery.Infrastructure.Repositories.GoDelivery;

public class RentalPlanRepository : BaseRepository<RentalPlanEntity>, IRentalPlanRepository
{
    public RentalPlanRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<RentalPlanEntity?> GetById(int id)
    {
        var result = await _context.Set<RentalPlanEntity>()
            .FirstOrDefaultAsync(x => x.Id == id);

        return result;
    }
}
