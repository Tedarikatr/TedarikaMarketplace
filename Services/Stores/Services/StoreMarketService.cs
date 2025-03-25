using AutoMapper;
using Data.Dtos.Stores;
using Entity.Stores.Market;
using Microsoft.Extensions.Logging;
using Repository.Markets.IRepositorys;
using Repository.Stores.IRepositorys;
using Services.Stores.IServices;

namespace Services.Stores.Services
{
    public class StoreMarketService : IStoreMarketService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IMarketRepository _marketRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<StoreMarketService> _logger;

        public StoreMarketService(
            IStoreRepository storeRepository,
            IMarketRepository marketRepository,
            IMapper mapper,
            ILogger<StoreMarketService> logger)
        {
            _storeRepository = storeRepository;
            _marketRepository = marketRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<StoreMarketAddDto>> GetAvailableMarketsForStoreAsync(int storeId)
        {
            var store = await _storeRepository.GetByIdAsync(storeId);
            if (store == null)
            {
                _logger.LogWarning("Mağaza bulunamadı. ID: {StoreId}", storeId);
                throw new Exception("Mağaza bulunamadı.");
            }

            var allMarkets = await _marketRepository.GetAllAsync();
            var storeMarketIds = store.StoreMarkets?.Select(sm => sm.MarketId).ToList() ?? new List<int>();

            var result = allMarkets.Select(m => new StoreMarketAddDto
            {
                Id = m.Id,
                Name = m.Name,
                IsAlreadyAdded = storeMarketIds.Contains(m.Id),
                IsActive = store.StoreMarkets?.FirstOrDefault(sm => sm.MarketId == m.Id)?.IsActive
            });

            return result;
        }

        public async Task<string> AddMarketToStoreAsync(int storeId, int marketId)
        {
            var store = await _storeRepository.GetByIdAsync(storeId);
            if (store == null) throw new Exception("Mağaza bulunamadı.");

            if (store.StoreMarkets?.Any(sm => sm.MarketId == marketId) == true)
                return "Bu market zaten mağazaya eklenmiş.";

            store.StoreMarkets ??= new List<StoreMarket>();
            store.StoreMarkets.Add(new StoreMarket
            {
                StoreId = storeId,
                MarketId = marketId,
                IsActive = true
            });

            await _storeRepository.UpdateAsync(store);
            return "Market mağazaya başarıyla eklendi.";
        }

        public async Task<string> RemoveMarketFromStoreAsync(int storeId, int marketId)
        {
            var store = await _storeRepository.GetByIdAsync(storeId);
            if (store == null) throw new Exception("Mağaza bulunamadı.");

            var marketEntry = store.StoreMarkets?.FirstOrDefault(sm => sm.MarketId == marketId);
            if (marketEntry == null) return "Market mağazaya eklenmemiş.";

            store.StoreMarkets.Remove(marketEntry);
            await _storeRepository.UpdateAsync(store);
            return "Market mağazadan kaldırıldı.";
        }

        public async Task<string> SetStoreMarketStatusAsync(int storeId, int marketId, bool isActive)
        {
            var store = await _storeRepository.GetByIdAsync(storeId);
            if (store == null) throw new Exception("Mağaza bulunamadı.");

            var marketEntry = store.StoreMarkets?.FirstOrDefault(sm => sm.MarketId == marketId);
            if (marketEntry == null) return "Market mağazaya eklenmemiş.";

            marketEntry.IsActive = isActive;
            await _storeRepository.UpdateAsync(store);
            return $"Market durumu {(isActive ? "aktif" : "pasif")} olarak güncellendi.";
        }
    }
}
