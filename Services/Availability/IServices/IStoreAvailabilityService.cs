using Data.Dtos.Availability;

namespace Services.Availability.IServices
{
    public interface IStoreAvailabilityService
    {
        Task<List<AvailableStoreDto>> GetAvailableStoresByAddressAsync(StoreAvailabilityFilterDto dto);

    }
}
