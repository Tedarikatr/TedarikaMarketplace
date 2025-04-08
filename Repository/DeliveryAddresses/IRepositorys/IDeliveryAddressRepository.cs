using Data.Repository;
using Entity.DeliveryAddresses;

namespace Repository.DeliveryAddresses.IRepositorys
{
    public interface IDeliveryAddressRepository : IGenericRepository<DeliveryAddress>
    {
        Task<IEnumerable<DeliveryAddress>> GetAddressesByBuyerIdAsync(int buyerUserId);
        Task<DeliveryAddress> GetDefaultAddressAsync(int buyerUserId);
    }
}
