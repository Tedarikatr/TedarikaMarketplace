using Data.Repository;
using Entity.Stores;

namespace Repository.Store.IRepositorys
{
    public interface IStoreRepository : IGenericRepository<Entity.Stores.Store>
    {
        Task<Entity.Stores.Store> GetStoreBySellerIdAsync(int sellerId);
        Task<bool> AddPaymentMethodAsync(StorePaymentMethod paymentMethod);
        Task<bool> AddDeliveryOptionAsync(StoreCarrier deliveryOption);
        Task<bool> AddMarketAsync(StoreMarket market);
    }
}
