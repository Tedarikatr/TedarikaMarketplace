using Domain.Stores.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Stores.Handlers
{
    public class StoreCoverageChangedEventHandler : INotificationHandler<StoreCoverageChangedEvent>
    {
        private readonly ILogger<StoreCoverageChangedEventHandler> _logger;

        public StoreCoverageChangedEventHandler(ILogger<StoreCoverageChangedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(StoreCoverageChangedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Store coverage değişti. StoreId: {StoreId}", notification.StoreId);
            return Task.CompletedTask;
        }
    }
}
