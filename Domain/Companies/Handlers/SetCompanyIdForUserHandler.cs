using Data.Databases;
using Domain.Companies.Events;
using Entity.Auths;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Companies.Handlers
{
    public class SetCompanyIdForUserHandler : INotificationHandler<CompanyCreatedEvent>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SetCompanyIdForUserHandler> _logger;

        public SetCompanyIdForUserHandler(ApplicationDbContext context, ILogger<SetCompanyIdForUserHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Handle(CompanyCreatedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                if (notification.UserType == UserType.Seller)
                {
                    var seller = await _context.SellerUsers.FindAsync(notification.UserId);
                    if (seller != null)
                    {
                        seller.CompanyId = notification.CompanyId;
                        await _context.SaveChangesAsync();
                        _logger.LogInformation("Satıcının CompanyId güncellendi: {UserId}", notification.UserId);
                    }
                }
                else if (notification.UserType == UserType.Buyer)
                {
                    var buyer = await _context.BuyerUsers.FindAsync(notification.UserId);
                    if (buyer != null)
                    {
                        buyer.CompanyId = notification.CompanyId;
                        await _context.SaveChangesAsync();
                        _logger.LogInformation("Alıcının CompanyId güncellendi: {UserId}", notification.UserId);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcının CompanyId güncellenirken hata oluştu.");
            }
        }
    }
}
