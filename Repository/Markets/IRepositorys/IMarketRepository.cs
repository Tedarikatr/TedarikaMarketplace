using Data.Repository;
using Entity.Markets;

namespace Repository.Markets.IRepositorys
{
    public interface IMarketRepository : IGenericRepository<Market>
    {
        Task<bool> AddStoreToMarketAsync(int storeId, int marketId);
    }
}
