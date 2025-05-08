using Data.Dtos.Availability;
using Data.Dtos.Stores.Products;

namespace Services.Availability.IServices
{
    public interface IStoreAvailabilityService
    {
        Task<List<AvailableStoreDto>> GetAvailableStoresByAddressAsync(int buyerId);

        Task<List<AvailableStoreWithProductsDto>> GetAvailableStoresWithProductsByAddressAsync(int buyerId);

        Task<List<StoreProductDto>> GetAvailableProductsByAddressAsync(int buyerId);

    }
}
