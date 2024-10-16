using CoreGoDelivery.Domain.Entities.GoDelivery.ModelMotocycle;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CoreGoDelivery.Infrastructure.Repositories.GoDelivery
{
    public class ModelMotocycleRepository : BaseRepository<ModelMotocycleEntity>, IModelMotocycleRepository
    {
        public ModelMotocycleRepository(AplicationDbContext context) : base(context)
        {
        }

        public async Task<string> GetIdByModelName(string model)
        {
            var result = await _context.Set<ModelMotocycleEntity>()
                .FirstOrDefaultAsync(x => x.NormalizedName == model);

            return result?.Id ?? "";
        }
    }
}
