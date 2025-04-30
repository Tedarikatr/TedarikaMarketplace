using Data.Repository;
using Entity.Stores;

namespace Repository.Stores
{
    public interface IStoreRepository : IGenericRepository<Store>
    {
        Task<Store> GetStoreBySellerIdAsync(int sellerId);
    }
}
