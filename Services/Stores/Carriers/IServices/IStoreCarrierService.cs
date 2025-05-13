using Data.Dtos.Stores.Carriers;

namespace Services.Stores.Carriers.IServices
{
    public interface IStoreCarrierService
    {
        Task<List<StoreCarrierDto>> GetStoreCarriersAsync(int storeId);
        Task<bool> AddCarrierToStoreAsync(StoreCarrierCreateDto dto);
        Task<bool> RemoveCarrierFromStoreAsync(int storeCarrierId);
        Task<bool> EnableCarrierAsync(int storeCarrierId);
        Task<bool> DisableCarrierAsync(int storeCarrierId);
    }
}
