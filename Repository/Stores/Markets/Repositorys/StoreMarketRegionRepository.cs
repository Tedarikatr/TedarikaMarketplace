using Data.Databases;
using Data.Repository;
using Entity.Stores.Markets;
using Microsoft.EntityFrameworkCore;
using Repository.Stores.Markets.IRepositorys;

namespace Repository.Stores.Markets.Repositorys
{
    public class StoreMarketRegionRepository : GenericRepository<StoreMarketRegion>, IStoreMarketRegionRepository
    {
        public StoreMarketRegionRepository(ApplicationDbContext context) : base(context) { }

        public async Task<List<StoreMarketRegion>> GetByStoreIdAsync(int storeId)
        {
            return await _dbSet.Where(x => x.StoreId == storeId).ToListAsync();
        }

        public async Task<bool> ExistsAsync(int storeId, string country, string province, string district)
        {
            return await _dbSet.AnyAsync(x =>
                x.StoreId == storeId &&
                x.Country == country &&
                x.Province == province &&
                x.District == district);
        }
    }
}
