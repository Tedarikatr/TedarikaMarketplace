using Data.Dtos.Availability;

namespace Services.Availability.IServices
{
    public interface IStoreAvailabilityService
    {
        Task<List<AvailableStoreDto>> GetAvailableStoresByAddressAsync(int buyerId);

        Task<List<AvailableStoreWithProductsDto>> GetAvailableStoresWithProductsByAddressAsync(int buyerId);
    }
}
