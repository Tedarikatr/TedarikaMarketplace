using Services.Notification.HelperService;
using Services.Notification.IServices;

namespace Services.Notification.Service
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly IPushSender _pushSender;
        private readonly IWebSocketSender _webSocketSender;

        public NotificationService(
            IEmailSender emailSender,
            ISmsSender smsSender,
            IPushSender pushSender,
            IWebSocketSender webSocketSender)
        {
            _emailSender = emailSender;
            _smsSender = smsSender;
            _pushSender = pushSender;
            _webSocketSender = webSocketSender;
        }

        public Task SendEmailAsync(string to, string subject, string body)
            => _emailSender.SendEmailAsync(to, subject, body);

        public Task SendSmsAsync(string toPhone, string message)
            => _smsSender.SendSmsAsync(toPhone, message);

        public Task SendPushAsync(int userId, string message)
            => _pushSender.SendPushAsync(userId, message);

        public Task SendWebSocketAsync(int userId, string message)
            => _webSocketSender.SendWebSocketAsync(userId, message);

        public async Task NotifyOrderCreatedAsync(int userId, int orderId)
        {
            var message = $"Siparişiniz başarıyla oluşturuldu. Sipariş No: {orderId}";
            await NotifyUserAsync(userId, message);
        }

        public async Task NotifyOrderApprovedAsync(int userId, int orderId)
        {
            var message = $"Siparişiniz onaylandı. Sipariş No: {orderId}";
            await NotifyUserAsync(userId, message);
        }

        public async Task NotifyOrderRejectedAsync(int userId, int orderId)
        {
            var message = $"Siparişiniz reddedildi. Sipariş No: {orderId}";
            await NotifyUserAsync(userId, message);
        }

        public async Task NotifyOrderShippedAsync(int userId, int orderId)
        {
            var message = $"Siparişiniz kargoya verildi. Sipariş No: {orderId}";
            await NotifyUserAsync(userId, message);
        }

        private async Task NotifyUserAsync(int userId, string message)
        {
            await Task.WhenAll(
                SendPushAsync(userId, message),
                SendWebSocketAsync(userId, message),
                SendSmsAsync("dummy-phone", message),
                SendEmailAsync("dummy@example.com", "Sipariş Bildirimi", message)
            );
        }
    }
}
