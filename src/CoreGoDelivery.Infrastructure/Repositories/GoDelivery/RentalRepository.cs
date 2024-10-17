using CoreGoDelivery.Domain.Entities.GoDelivery.Rental;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CoreGoDelivery.Infrastructure.Repositories.GoDelivery
{
    public class RentalRepository : BaseRepository<RentalEntity>, IRentalRepository
    {
        public RentalRepository(AplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> Create(RentalEntity data)
        {
            var result = await _context
                .Set<RentalEntity>()
                .AddAsync(data);

            await _context.SaveChangesAsync();

            return IsSuccessCreate(result);
        }

        public async Task<RentalEntity?> FindByMotorcycleId(string id)
        {
            var result = await _context.Set<RentalEntity>()
                .FirstOrDefaultAsync(x => x.MotorcycleId == id);

            return result;
        }

        public async Task<RentalEntity?> GetByIdAsync(string id)
        {
            var result = await _context.Set<RentalEntity>()
                .Include(x => x.RentalPlan)
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<bool> UpdateReturnedToBaseDate(string id, DateTime date)
        {
            if (string.IsNullOrEmpty(id) || id == ":id")
            {
                return false;
            }

            var rentalEntity = await _context.Set<RentalEntity>()
                .AsTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (rentalEntity == null)
            {
                return false;
            }
 
                rentalEntity.ReturnedToBaseDate = date;

                await _context.SaveChangesAsync();

                return true;

            
        }
    }
}
