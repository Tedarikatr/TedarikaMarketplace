namespace Services.Notification.IServices
{
    public interface INotificationService
    {
        Task SendEmailAsync(string to, string subject, string body);
        Task SendSmsAsync(string toPhone, string message);
        Task SendPushAsync(int userId, string message);
        Task SendWebSocketAsync(int userId, string message);

        Task NotifyOrderCreatedAsync(int userId, int orderId);
        Task NotifyOrderApprovedAsync(int userId, int orderId);
        Task NotifyOrderRejectedAsync(int userId, int orderId);
        Task NotifyOrderShippedAsync(int userId, int orderId);
    }
}
