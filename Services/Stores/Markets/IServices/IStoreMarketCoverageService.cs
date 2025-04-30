using Data.Dtos.Stores.Markets;

namespace Services.Stores.Markets.IServices
{
    public interface IStoreMarketCoverageService
    {
        // Seller işlemleri
        Task<List<StoreMarketCoverageDto>> GetMyCoveragesAsync(int storeId, bool? onlyActive = null);
        Task<List<StoreMarketCoverageDto>> GetSellerOwnCoveragesAsync(int sellerUserId);
        Task AddCoverageAsync(StoreMarketCoverageCreateDto dto, int storeId);
        Task AddCoverageMultiAsync(StoreMarketCoverageMultiCreateDto dto);
        Task AddCoveragesBulkAsync(StoreMarketCoverageBatchDto dto);
        Task UpdateCoverageAsync(StoreMarketCoverageUpdateDto dto, int storeId);
        Task RemoveCoverageAsync(int coverageId, int storeId);
        Task ActivateCoverageAsync(int coverageId, int storeId);
        Task DeactivateCoverageAsync(int coverageId, int storeId);

        // Buyer işlemleri
        Task<List<StoreMarketCoverageDto>> GetCoveragesMatchingBuyerAddressAsync(int buyerId, int addressId);

        // Admin işlemleri
        Task<List<StoreMarketCoverageDto>> GetAllCoveragesAsync();
        Task<List<StoreMarketCoverageDto>> GetCoveragesByStoreIdAsync(int storeId);
        Task AddCoverageAsAdminAsync(StoreMarketCoverageCreateDto dto, int storeId);
        Task AddCoveragesAsAdminBulkAsync(StoreMarketCoverageBatchDto dto);
        Task RemoveCoverageAsAdminAsync(int coverageId);
    }
}
