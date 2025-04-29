using Data.Databases;
using Data.Repository;
using Entity.Stores.Carriers;
using Entity.Stores.Markets;
using Entity.Stores.Payments;
using Microsoft.EntityFrameworkCore;

namespace Repository.Stores
{
    public class StoreRepository : GenericRepository<Entity.Stores.Store>, IStoreRepository
    {
        public StoreRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Entity.Stores.Store> GetStoreBySellerIdAsync(int sellerId)
        {
            return await _dbSet
                .Include(s => s.Company)
                .Include(s => s.StoreMarkets)
                .FirstOrDefaultAsync(s => s.SellerId == sellerId);
        }

        public async Task<bool> AddPaymentMethodAsync(StorePaymentMethod paymentMethod)
        {
            await _context.Set<StorePaymentMethod>().AddAsync(paymentMethod);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddDeliveryOptionAsync(StoreCarrier deliveryOption)
        {
            await _context.Set<StoreCarrier>().AddAsync(deliveryOption);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddMarketAsync(StoreMarket market)
        {
            await _context.Set<StoreMarket>().AddAsync(market);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
