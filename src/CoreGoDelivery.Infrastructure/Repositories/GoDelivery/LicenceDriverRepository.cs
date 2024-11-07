using CoreGoDelivery.Domain.Entities.GoDelivery.LicenceDriver;
using CoreGoDelivery.Domain.Repositories.GoDelivery;
using CoreGoDelivery.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CoreGoDelivery.Infrastructure.Repositories.GoDelivery;

public class LicenceDriverRepository : BaseRepository<LicenceDriverEntity>, ILicenceDriverRepository
{
    public LicenceDriverRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> CheckIsUnicByLicence(string id)
    {
        var result = await _context
            .Set<LicenceDriverEntity>()
            .FirstOrDefaultAsync(x => x.Id == id);

        return IsUnic(result);
    }

    public async Task<bool> Create(LicenceDriverEntity data)
    {
        var result = await _context
            .Set<LicenceDriverEntity>()
            .AddAsync(data);

        await _context.SaveChangesAsync();

        return IsSuccessCreate(result);
    }

    public async Task<bool> UpdateFileName(string id, string fileName)
    {
        var entity = await _context.Set<LicenceDriverEntity>()
            .AsTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null)
        {
            return false;
        }

        entity.ImageUrlReference = fileName;

        return true;
    }
}
