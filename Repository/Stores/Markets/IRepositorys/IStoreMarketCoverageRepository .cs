using Data.Repository;
using Entity.Stores.Markets;
using System.Linq.Expressions;

namespace Repository.Stores.Markets.IRepositorys
{
    public interface IStoreMarketCoverageRepository : IGenericRepository<StoreMarketCoverage>
    {
        Task<List<StoreMarketCoverage>> FindWithStoreMarketAsync(Expression<Func<StoreMarketCoverage, bool>> predicate);

    }
}
