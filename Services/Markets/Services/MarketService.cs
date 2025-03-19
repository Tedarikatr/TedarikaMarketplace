using AutoMapper;
using Data.Dtos.Markets;
using Microsoft.Extensions.Logging;
using Repository.Markets.IRepositorys;

namespace Services.Market.Services
{
    public class MarketService : IMarketService
    {
        private readonly IMarketRepository _marketRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MarketService> _logger;

        public MarketService(IMarketRepository marketRepository, IMapper mapper, ILogger<MarketService> logger)
        {
            _marketRepository = marketRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<MarketDto>> GetAllMarketsAsync()
        {
            _logger.LogInformation("Tüm marketler listeleniyor.");
            var markets = await _marketRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MarketDto>>(markets);
        }

        public async Task<MarketDto> GetMarketByIdAsync(int marketId)
        {
            _logger.LogInformation("Market detayları alınıyor. Market ID: {MarketId}", marketId);
            var market = await _marketRepository.GetByIdAsync(marketId);
            if (market == null) throw new Exception("Market bulunamadı.");
            return _mapper.Map<MarketDto>(market);
        }

        public async Task<string> CreateMarketAsync(MarketCreateDto marketDto)
        {
            try
            {
                _logger.LogInformation("Yeni market oluşturuluyor. Market Adı: {MarketName}", marketDto.Name);

                var newMarket = _mapper.Map<Entity.Markets.Market>(marketDto);
                newMarket.IsActive = true;

                await _marketRepository.AddAsync(newMarket);
                _logger.LogInformation("Market başarıyla oluşturuldu. Market ID: {MarketId}", newMarket.Id);
                return "Market başarıyla oluşturuldu.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Market oluşturma sırasında hata oluştu.");
                throw;
            }
        }

        public async Task<string> UpdateMarketAsync(int marketId, MarketUpdateDto marketDto)
        {
            try
            {
                _logger.LogInformation("Market güncelleniyor. Market ID: {MarketId}", marketId);

                var market = await _marketRepository.GetByIdAsync(marketId);
                if (market == null) throw new Exception("Market bulunamadı.");

                _mapper.Map(marketDto, market);
                await _marketRepository.UpdateAsync(market);

                _logger.LogInformation("Market güncellendi. Market ID: {MarketId}", marketId);
                return "Market başarıyla güncellendi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Market güncelleme sırasında hata oluştu.");
                throw;
            }
        }

        public async Task<string> SetMarketStatusAsync(int marketId, bool isActive)
        {
            try
            {
                _logger.LogInformation("Market durumu değiştiriliyor. Market ID: {MarketId}, Yeni Durum: {IsActive}", marketId, isActive);

                var market = await _marketRepository.GetByIdAsync(marketId);
                if (market == null) throw new Exception("Market bulunamadı.");

                market.IsActive = isActive;
                await _marketRepository.UpdateAsync(market);

                _logger.LogInformation("Market durumu değiştirildi. Market ID: {MarketId}", marketId);
                return "Market durumu başarıyla değiştirildi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Market durumu değiştirirken hata oluştu.");
                throw;
            }
        }

        public async Task<string> AddStoreToMarketAsync(int storeId, int marketId)
        {
            try
            {
                _logger.LogInformation("Mağaza markete ekleniyor. Store ID: {StoreId}, Market ID: {MarketId}", storeId, marketId);

                var result = await _marketRepository.AddStoreToMarketAsync(storeId, marketId);
                if (!result) throw new Exception("Mağaza markete eklenemedi.");

                _logger.LogInformation("Mağaza markete başarıyla eklendi. Store ID: {StoreId}, Market ID: {MarketId}", storeId, marketId);
                return "Mağaza başarıyla markete eklendi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Mağaza markete eklenirken hata oluştu.");
                throw;
            }
        }
    }
}
