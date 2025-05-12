using Data.Repository;
using Entity.DeliveryAddresses;

namespace Repository.DeliveryAddresses.IRepositorys
{
    public interface IDeliveryAddressRepository : IGenericRepository<DeliveryAddress>
    {
        Task<List<DeliveryAddress>> GetAllWithLocationByBuyerIdAsync(int buyerUserId);
        Task<DeliveryAddress> GetWithLocationByIdAsync(int id);
        Task<DeliveryAddress> GetDefaultWithLocationByBuyerIdAsync(int buyerUserId);
    }
}
