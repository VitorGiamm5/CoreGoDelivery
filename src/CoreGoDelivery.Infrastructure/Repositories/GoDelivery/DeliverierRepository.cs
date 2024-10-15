using CoreGoDelivery.Domain.Entities.GoDelivery.Deliverier;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CoreGoDelivery.Infrastructure.Repositories.GoDelivery
{
    public class DeliverierRepository : BaseRepository<DeliverierEntity>, IDeliverierRepository
    {
        public DeliverierRepository(AplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> CheckIsUnicById(string id)
        {
            var result = await _context.Set<DeliverierEntity>()
                .FirstOrDefaultAsync(x => x.Id == id);

            return result == null;
        }

        public async Task<bool> CheckIsUnicByCnpj(string id)
        {
            var result = await _context.Set<DeliverierEntity>()
                .FirstOrDefaultAsync(x => x.CNPJ == id);

            return result == null;
        }

        public async Task<bool> Create(DeliverierEntity data)
        {
            var result = await _context.Set<DeliverierEntity>().AddAsync(data);
            await _context.SaveChangesAsync();

            return result != null;
        }
    }
}
