using Data.Dtos.Store;

namespace Services.Store.IServices
{
    public interface IStoreService
    {
        Task<string> CreateStoreAsync(StoreCreateDto storeCreateDto, int sellerId);
        Task<string> UpdateStoreAsync(StoreUpdateDto storeUpdateDto, int storeId, int sellerId);
        Task<string> SetStoreStatusAsync(int storeId, bool isActive, int sellerId);
    }
}
