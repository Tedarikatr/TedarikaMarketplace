using Domain.Orders.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Services.Carriers.IServices;

namespace Domain.Orders.Handlers
{
    public class OrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
    {
        private readonly ICarrierWebhookService _webhookService;
        private readonly ILogger<OrderCreatedEventHandler> _logger;

        public OrderCreatedEventHandler(ICarrierWebhookService webhookService, ILogger<OrderCreatedEventHandler> logger)
        {
            _webhookService = webhookService;
            _logger = logger;
        }

        public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var order = notification.Order;
                if (order.SelectedCarrierId == null)
                {
                    _logger.LogWarning("Sipariş taşıyıcısı tanımlı değil. OrderId: {OrderId}", order.Id);
                    return;
                }

                await _webhookService.SendOrderToCarrierAsync(order);
                _logger.LogInformation("OrderCreatedEvent için webhook gönderimi yapıldı. OrderId: {OrderId}", order.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "OrderCreatedEvent işlenirken hata oluştu.");
            }
        }
    }
}
