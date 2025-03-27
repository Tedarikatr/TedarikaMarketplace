namespace Services.Notification.HelperService
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string to, string message);
    }

    public class SmsSender : ISmsSender
    {
        public async Task SendSmsAsync(string to, string message)
        {
            Console.WriteLine($"SMS gönderildi -> {to} - {message}");
            await Task.CompletedTask;
        }
    }
}
