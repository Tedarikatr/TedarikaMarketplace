using Data.Databases;
using Data.Repository;
using Entity.Stores.Locations;
using Microsoft.EntityFrameworkCore;
using Repository.Stores.Locations.IRepositorys;

namespace Repository.Stores.Locations.Repositorys
{
    public class StoreCoverageRepository : GenericRepository<StoreCoverage>, IStoreCoverageRepository
    {
        public StoreCoverageRepository(ApplicationDbContext context) : base(context) { }

        public async Task<List<StoreCoverage>> GetByStoreIdAsync(int storeId)
        {
            return await _context.StoreCoverages
                .Where(c => c.StoreId == storeId)
                .ToListAsync();
        }

        public async Task DeleteCascadeForStoreAsync(int storeId, int? countryId, int? provinceId)
        {
            var entries = _context.StoreCoverages.Where(c => c.StoreId == storeId);

            if (countryId.HasValue)
                entries = entries.Where(c => c.CountryIds.Contains(countryId.Value));

            if (provinceId.HasValue)
                entries = entries.Where(c => c.ProvinceIds.Contains(provinceId.Value));

            _context.StoreCoverages.RemoveRange(entries);
            await _context.SaveChangesAsync();
        }
    }
}
