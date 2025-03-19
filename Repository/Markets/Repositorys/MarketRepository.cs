using Data.Databases;
using Data.Repository;
using Entity.Markets;
using Entity.Stores;
using Microsoft.EntityFrameworkCore;
using Repository.Markets.IRepositorys;

namespace Repository.Markets.Repositorys
{
    public class MarketRepository : GenericRepository<Market>, IMarketRepository
    {
        public MarketRepository(ApplicationDbContext context) : base(context) { }

        public async Task<bool> AddStoreToMarketAsync(int storeId, int marketId)
        {
            var market = await _dbSet.Include(m => m.StoreMarkets).FirstOrDefaultAsync(m => m.Id == marketId);
            if (market == null) return false;

            if (market.StoreMarkets.Any(sm => sm.StoreId == storeId)) return false;

            market.StoreMarkets.Add(new StoreMarket { StoreId = storeId, MarketId = marketId });
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
