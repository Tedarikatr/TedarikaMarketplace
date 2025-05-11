using Domain.Stores.Events;
using Domain.Stores.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Stores.Handlers
{
    public class StoreLocationCoverageChangedHandler : INotificationHandler<StoreLocationCoverageChangedEvent>
    {
        private readonly StoreLocationCoverageUpdatedHelper _coverageHandler;
        private readonly ILogger<StoreLocationCoverageChangedHandler> _logger;

        public StoreLocationCoverageChangedHandler(
            StoreLocationCoverageUpdatedHelper coverageHandler,
            ILogger<StoreLocationCoverageChangedHandler> logger)
        {
            _coverageHandler = coverageHandler;
            _logger = logger;
        }

        public async Task Handle(StoreLocationCoverageChangedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("📣 StoreLocationCoverageChangedEvent tetiklendi. StoreId: {StoreId}", notification.StoreId);
            var result = await _coverageHandler.UpdateCoverageAsync(notification.StoreId);
            if (result)
                _logger.LogInformation("✅ StoreLocationCoverage senkronizasyonu tamamlandı.");
            else
                _logger.LogWarning("⚠️ StoreLocationCoverage senkronizasyonu başarısız oldu.");
        }
    }
}
