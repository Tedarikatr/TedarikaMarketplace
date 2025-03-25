using AutoMapper;
using Data.Dtos.Markets;
using Domain.Markets.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Repository.Markets.IRepositorys;
using Services.Markets.IServices;

namespace Services.Markets.Services
{
    public class MarketService : IMarketService
    {
        private readonly IMarketRepository _marketRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MarketService> _logger;
        private readonly IMediator _mediator;


        public MarketService(IMarketRepository marketRepository, IMapper mapper, ILogger<MarketService> logger)
        {
            _marketRepository = marketRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<MarketDto>> GetAllMarketsAsync()
        {
            try
            {
                _logger.LogInformation("Tüm marketler listeleniyor.");
                var markets = await _marketRepository.GetAllAsync();

                if (markets == null || !markets.Any())
                {
                    _logger.LogWarning("Sistemde kayıtlı hiçbir market bulunamadı.");
                    throw new Exception("Sistemde kayıtlı market bulunamadı.");
                }

                _logger.LogInformation("{MarketCount} adet market bulundu.", markets.Count());
                return _mapper.Map<IEnumerable<MarketDto>>(markets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Marketleri çekerken hata oluştu.");
                throw new Exception("Marketleri çekerken bir hata oluştu, lütfen tekrar deneyin.");
            }
        }

        public async Task<MarketDto> GetMarketByIdAsync(int marketId)
        {
            try
            {
                _logger.LogInformation("Market detayları alınıyor. Market ID: {MarketId}", marketId);

                var market = await _marketRepository.GetByIdAsync(marketId);

                if (market == null)
                {
                    _logger.LogWarning("Market bulunamadı. Market ID: {MarketId}", marketId);
                    throw new Exception("Belirtilen market bulunamadı.");
                }

                _logger.LogInformation("Market bilgisi başarıyla getirildi. Market ID: {MarketId}", marketId);
                return _mapper.Map<MarketDto>(market);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Market bilgisi alınırken hata oluştu. Market ID: {MarketId}", marketId);
                throw new Exception("Market bilgisi alınırken bir hata oluştu, lütfen tekrar deneyin.");
            }
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

                // Event yayını
                var @event = new MarketUpdatedEvent(market.Id, market.Name, market.RegionCode);
                await _mediator.Publish(@event);

                _logger.LogInformation("Market güncellendi ve event tetiklendi. Market ID: {MarketId}", marketId);
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
    }
}
