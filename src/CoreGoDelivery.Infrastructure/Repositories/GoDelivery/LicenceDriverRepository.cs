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
        return IsUnic(await GetOneById(id));
    }

    public async Task<LicenceDriverEntity?> GetOneById(string id)
    {
        var result = await _context
            .Set<LicenceDriverEntity>()
            .FirstOrDefaultAsync(x => x.Id == id);

        return result;
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
        entity.DateUpdated = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
