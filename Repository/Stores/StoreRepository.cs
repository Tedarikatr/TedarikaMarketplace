using Data.Databases;
using Data.Repository;
using Entity.Stores;
using Microsoft.EntityFrameworkCore;

namespace Repository.Stores
{
    public class StoreRepository : GenericRepository<Store>, IStoreRepository
    {
        public StoreRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Store> GetStoreBySellerIdAsync(int sellerId)
        {
            return await _context.Stores
                .FirstOrDefaultAsync(s => s.SellerId == sellerId);
        }
    }
}
