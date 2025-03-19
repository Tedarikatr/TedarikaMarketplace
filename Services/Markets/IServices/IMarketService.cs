using Data.Dtos.Markets;

namespace Services.Markets.IServices
{
    public interface IMarketService
    {
        Task<IEnumerable<MarketDto>> GetAllMarketsAsync();
        Task<MarketDto> GetMarketByIdAsync(int marketId);
        Task<string> CreateMarketAsync(MarketCreateDto marketDto);
        Task<string> UpdateMarketAsync(int marketId, MarketUpdateDto marketDto);
        Task<string> SetMarketStatusAsync(int marketId, bool isActive);
        Task<string> AddStoreToMarketAsync(int storeId, int marketId);
    }
}
