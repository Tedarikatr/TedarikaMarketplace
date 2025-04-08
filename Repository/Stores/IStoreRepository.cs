using Data.Repository;
using Entity.Stores;
using Entity.Stores.Carriers;
using Entity.Stores.Markets;
using Entity.Stores.Payments;

namespace Repository.Stores
{
    public interface IStoreRepository : IGenericRepository<Store>
    {
        Task<Store> GetStoreBySellerIdAsync(int sellerId);
        Task<bool> AddPaymentMethodAsync(StorePaymentMethod paymentMethod);
        Task<bool> AddDeliveryOptionAsync(StoreCarrier deliveryOption);
        Task<bool> AddMarketAsync(StoreMarket market);
    }
}
