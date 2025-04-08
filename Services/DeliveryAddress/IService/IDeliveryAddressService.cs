namespace Services.DeliveryAddress.IService
{
    public interface IDeliveryAddressService
    {
        Task<IEnumerable<Entity.DeliveryAddresses.DeliveryAddress>> GetAddressesByBuyerAsync(int buyerUserId);
        Task<Entity.DeliveryAddresses.DeliveryAddress> GetAddressByIdAsync(int id);
        Task<Entity.DeliveryAddresses.DeliveryAddress> GetDefaultAddressAsync(int buyerUserId);
        Task AddAddressAsync(Entity.DeliveryAddresses.DeliveryAddress address);
        Task UpdateAddressAsync(Entity.DeliveryAddresses.DeliveryAddress address);
        Task DeleteAddressAsync(int id);
        Task SetAsDefaultAsync(int buyerUserId, int addressId);
    }
}
