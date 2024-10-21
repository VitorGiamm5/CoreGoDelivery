using CoreGoDelivery.Domain.Entities.GoDelivery.ModelMotorcycle;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CoreGoDelivery.Infrastructure.Repositories.GoDelivery
{
    public class ModelMotocycleRepository : BaseRepository<ModelMotorcycleEntity>, IModelMotocycleRepository
    {
        public ModelMotocycleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<string> GetIdByModelName(string model)
        {
            var result = await _context.Set<ModelMotorcycleEntity>()
                .FirstOrDefaultAsync(x => x.NormalizedName == model);

            return result?.Id ?? "";
        }
    }
}
