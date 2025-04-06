using Data.Dtos.DeliveryAddresses;

namespace Services.DeliveryAddress.IService
{
    public interface IDeliveryAddressService
    {
        Task<List<DeliveryAddressDto>> GetAddressesByBuyerIdAsync(int buyerId);
        Task<DeliveryAddressDto> AddAddressAsync(DeliveryAddressCreateDto dto);
        Task<bool> UpdateAddressAsync(DeliveryAddressUpdateDto dto);
        Task<bool> DeleteAddressAsync(int id);
        Task<bool> SetDefaultAsync(int buyerId, int addressId);
    }
}
