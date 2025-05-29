namespace Services.Notification.IServices
{
    public interface INotificationService
    {
        Task WelcomeBuyerSendEmailAsync(string to, string userName);
        Task WelcomeSellerSendEmailAsync(string to, string userName);
    }
}
