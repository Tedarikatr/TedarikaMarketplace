using Data.Databases;
using Data.Repository;
using Entity.Stores;
using Microsoft.EntityFrameworkCore;
using Repository.Stores.IRepositorys;

namespace Repository.Stores.Repositorys
{
    public class StoreProductRepository : GenericRepository<StoreProduct>, IStoreProductRepository
    {
        public StoreProductRepository(ApplicationDbContext context) : base(context) { }

        public async Task<StoreProduct> GetByStoreAndProductIdAsync(int storeId, int productId)
        {
            return await _dbSet.FirstOrDefaultAsync(sp => sp.StoreId == storeId && sp.ProductId == productId);
        }
    }
}
