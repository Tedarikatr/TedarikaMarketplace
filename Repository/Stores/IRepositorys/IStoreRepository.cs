using Data.Repository;
using Entity.Stores;

namespace Repository.Stores.IRepositorys
{
    public interface IStoreRepository : IGenericRepository<Store>
    {
        Task<Store> GetStoreBySellerIdAsync(int sellerId);
        Task<bool> AddPaymentMethodAsync(StorePaymentMethod paymentMethod);
        Task<bool> AddDeliveryOptionAsync(StoreCarrier deliveryOption);
        Task<bool> AddMarketAsync(StoreMarket market);
    }
}
