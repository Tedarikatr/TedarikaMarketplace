using Data.Repository;
using Entity.DeliveryAddresses;

namespace Repository.DeliveryAddresses.IRepositorys
{
    public interface IDeliveryAddressRepository : IGenericRepository<Entity.DeliveryAddresses.DeliveryAddress>
    {
        Task<DeliveryAddress> GetByIdWithBuyerAsync(int id, int buyerUserId);
    }
}
