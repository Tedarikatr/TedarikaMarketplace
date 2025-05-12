using Data.Repository;
using Entity.Stores.Products;
using Microsoft.EntityFrameworkCore;

namespace Repository.Stores.Product.IRepositorys
{
    public interface IStoreProductRepository : IGenericRepository<StoreProduct>
    {
        Task<StoreProduct> GetByStoreAndProductIdAsync(int storeId, int productId);
        Task<List<StoreProduct>> GetProductsByStoreIdsAsync(List<int> storeIds);
        Task<bool> UpdateMinMaxOrderQuantityAsync(int storeId, int productId, int minOrderQuantity, int maxOrderQuantity);
    }
}