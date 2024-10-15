using CoreGoDelivery.Domain.Entities.GoDelivery.LicenceDriver;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Infrastructure.Database;

namespace CoreGoDelivery.Infrastructure.Repositories.GoDelivery
{
    public class LicenceDriverRepository :BaseRepository<LicenceDriverEntity>, ILicenceDriverRepository
    {
        public LicenceDriverRepository(AplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> Create(LicenceDriverEntity data)
        {
            var result = await _context
                .Set<LicenceDriverEntity>()
                .AddAsync(data);

            await _context.SaveChangesAsync();

            return result != null;
        }
    }
}
