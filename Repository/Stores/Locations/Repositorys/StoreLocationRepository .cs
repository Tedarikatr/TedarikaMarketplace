using Data.Databases;
using Data.Repository;
using Entity.Stores.Locations;
using Microsoft.EntityFrameworkCore;
using Repository.Stores.Locations.IRepositorys;

namespace Repository.Stores.Locations.Repositorys
{
    public class StoreLocationCoverageRepository : GenericRepository<StoreCovargeLocation>, IStoreLocationCoverageRepository
    {
        public StoreLocationCoverageRepository(ApplicationDbContext context) : base(context) { }

        public async Task<StoreCovargeLocation> GetByStoreIdAsync(int storeId)
        {
            return await _context.StoreCovargeLocations
                .FirstOrDefaultAsync(c => c.StoreId == storeId);
        }
    }

    
}
