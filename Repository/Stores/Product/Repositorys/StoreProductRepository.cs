using Data.Databases;
using Data.Repository;
using Entity.Stores.Products;
using Microsoft.EntityFrameworkCore;
using Repository.Stores.Product.IRepositorys;

namespace Repository.Stores.Product.Repositorys
{
    public class StoreProductRepository : GenericRepository<StoreProduct>, IStoreProductRepository
    {
        public StoreProductRepository(ApplicationDbContext context) : base(context) { }

        public async Task<StoreProduct> GetByStoreAndProductIdAsync(int storeId, int productId)
        {
            return await _dbSet.FirstOrDefaultAsync(sp => sp.StoreId == storeId && sp.Id == productId);
        }

        public async Task<List<StoreProduct>> GetProductsByStoreIdsAsync(List<int> storeIds)
        {
            return await _dbSet
                .Include(p => p.Product)
                .Where(p => storeIds.Contains(p.StoreId))
                .ToListAsync();
        }

        public async Task<bool> UpdateMinMaxOrderQuantityAsync(int storeId, int productId, int minOrderQuantity, int maxOrderQuantity)
        {
            var product = await _context.StoreProducts
                .SingleOrDefaultAsync(p => p.StoreId == storeId && p.Id == productId);

            if (product == null)
                return false;

            product.MinOrderQuantity = minOrderQuantity;
            product.MaxOrderQuantity = maxOrderQuantity;

            _context.StoreProducts.Update(product);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
