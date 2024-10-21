using CoreGoDelivery.Domain.Entities.GoDelivery.Deliverier;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CoreGoDelivery.Infrastructure.Repositories.GoDelivery
{
    public class DeliverierRepository : BaseRepository<DeliverierEntity>, IDeliverierRepository
    {
        public DeliverierRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> CheckIsUnicById(string id)
        {
            var result = await _context.Set<DeliverierEntity>()
                .FirstOrDefaultAsync(x => x.Id == id);

            return Ok(result);
        }

        public async Task<DeliverierEntity?> GetOneById(string id)
        {
            var result = await _context.Set<DeliverierEntity>()
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<bool> CheckIsUnicByCnpj(string id)
        {
            var result = await _context.Set<DeliverierEntity>()
                .FirstOrDefaultAsync(x => x.CNPJ == id);

            return Ok(result);
        }

        public async Task<bool> Create(DeliverierEntity data)
        {
            var result = await _context
                .Set<DeliverierEntity>()
                .AddAsync(data);

            await _context.SaveChangesAsync();

            return IsSuccessCreate(result);
        }
    }
}
