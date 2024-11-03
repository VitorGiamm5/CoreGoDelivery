using CoreGoDelivery.Domain.Entities.GoDelivery.Rental;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CoreGoDelivery.Infrastructure.Repositories.GoDelivery;

public class RentalRepository : BaseRepository<RentalEntity>, IRentalRepository
{
    public RentalRepository(ApplicationDbContext context) : base(context)
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

    public async Task<bool> CheckMotorcycleIsAvaliable(string id)
    {
        var result = await _context.Set<RentalEntity>()
            .AnyAsync(x => x.MotorcycleId == id && x.ReturnedToBaseDate == null);

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

    public async Task<RentalEntity?> FindByMotorcycleId(string id)
    {
        var result = await _context.Set<RentalEntity>()
            .FirstOrDefaultAsync(x => x.MotorcycleId == id);

        return result;
    }

    public async Task<bool> CheckisReturnedById(string id)
    {
        var rental = await _context.Set<RentalEntity>()
            .FirstOrDefaultAsync(x => x.Id == id);

        var isReturned = rental?.ReturnedToBaseDate != null;

        return isReturned;
    }
}
