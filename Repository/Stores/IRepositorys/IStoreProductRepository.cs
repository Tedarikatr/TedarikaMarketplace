using Data.Repository;
using Entity.Stores.Products;

namespace Repository.Stores.IRepositorys
{
    public interface IStoreProductRepository : IGenericRepository<StoreProduct>
    {
        Task<StoreProduct> GetByStoreAndProductIdAsync(int storeId, int productId);
    }
}
