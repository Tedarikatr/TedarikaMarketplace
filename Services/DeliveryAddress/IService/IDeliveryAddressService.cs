using Data.Dtos.DeliveryAddresses;

namespace Services.DeliveryAddress.IService
{
    public interface IDeliveryAddressService
    {
        Task<IEnumerable<DeliveryAddressDto>> GetAddressesByBuyerAsync(int buyerUserId);
        Task<DeliveryAddressDto> GetAddressByIdAsync(int id);
        Task<DeliveryAddressDto> GetDefaultAddressAsync(int buyerUserId);
        Task<bool> AddAddressAsync(DeliveryAddressCreateDto dto, int buyerUserId);
        Task<bool> UpdateAddressAsync(DeliveryAddressUpdateDto dto, int buyerUserId);
        Task<bool> DeleteAddressAsync(int id, int buyerUserId);
        Task<bool> SetAsDefaultAsync(int buyerUserId, int addressId);
    }
}
