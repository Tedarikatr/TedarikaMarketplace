using Domain.Markets.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Repository.Markets.IRepositorys;

namespace Domain.Markets.Handlers
{
    public class MarketUpdatedEventHandler : INotificationHandler<MarketUpdatedEvent>
    {
        private readonly IMarketRepository _shopMarketRepository;
        private readonly ILogger<MarketUpdatedEventHandler> _logger;

        public MarketUpdatedEventHandler(IMarketRepository shopMarketRepository, ILogger<MarketUpdatedEventHandler> logger)
        {
            _shopMarketRepository = shopMarketRepository;
            _logger = logger;
        }

        public async Task Handle(MarketUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("MarketUpdatedEvent alındı. MarketId: {MarketId}", notification.MarketId);

            var shopMarkets = await _shopMarketRepository.FindAsync(sm => sm.Id == notification.MarketId);
            foreach (var shopMarket in shopMarkets)
            {
                shopMarket.Name = notification.Name;
                shopMarket.RegionCode = notification.RegionCode;
            }

            await _shopMarketRepository.UpdateRangeAsync(shopMarkets);

            _logger.LogInformation("ShopMarket kayıtları güncellendi. Etkilenen kayıt sayısı: {Count}", shopMarkets.Count());
        }
    }
}
