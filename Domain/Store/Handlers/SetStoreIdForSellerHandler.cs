using Data.Databases;
using Domain.Store.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Store.Handlers
{
    public class SetStoreIdForSellerHandler : INotificationHandler<StoreCreatedEvent>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SetStoreIdForSellerHandler> _logger;

        public SetStoreIdForSellerHandler(ApplicationDbContext context, ILogger<SetStoreIdForSellerHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Handle(StoreCreatedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var seller = await _context.SellerUsers.FindAsync(notification.SellerUserId);
                if (seller != null)
                {
                    seller.StoreId = notification.StoreId;
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Satıcının StoreId bilgisi güncellendi. SellerUserId: {SellerUserId}, StoreId: {StoreId}", notification.SellerUserId, notification.StoreId);
                }
                else
                {
                    _logger.LogWarning("StoreCreatedEvent işlenirken seller bulunamadı. SellerUserId: {SellerUserId}", notification.SellerUserId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "StoreCreatedEvent işlenirken hata oluştu.");
            }
        }
    }
}
