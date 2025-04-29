using Data.Databases;
using Data.Repository;
using Entity.Stores.Markets;
using Microsoft.EntityFrameworkCore;
using Repository.Stores.Markets.IRepositorys;
using System.Linq.Expressions;

namespace Repository.Stores.Markets.Repositorys
{
    public class StoreMarketCoverageRepository : GenericRepository<StoreMarketCoverage>, IStoreMarketCoverageRepository
    {
        public StoreMarketCoverageRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<StoreMarketCoverage>> FindWithStoreMarketAsync(Expression<Func<StoreMarketCoverage, bool>> predicate)
        {
            return await _context.StoreMarketCoverages
                                 .Include(x => x.StoreMarket)
                                 .Where(predicate)
                                 .ToListAsync();
        }

    }
}
