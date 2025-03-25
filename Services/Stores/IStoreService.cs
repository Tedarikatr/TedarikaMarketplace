using Data.Dtos.Stores;

namespace Services.Stores
{
    public interface IStoreService
    {
        Task<string> CreateStoreAsync(StoreCreateDto storeCreateDto, int sellerId);
        Task<string> UpdateStoreAsync(StoreUpdateDto storeUpdateDto, int storeId, int sellerId);
        Task<string> SetStoreStatusAsync(int storeId, bool isActive, int sellerId);
        Task<IEnumerable<StoreDto>> GetAllStoresAsync();
        Task<string> ApproveStoreAsync(int storeId, bool isApproved);
    }
}
