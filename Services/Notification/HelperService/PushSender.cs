namespace Services.Notification.HelperService
{
    public interface IPushSender
    {
        Task SendPushAsync(int userId, string message);
    }

    public class PushSender : IPushSender
    {
        public async Task SendPushAsync(int userId, string message)
        {
            Console.WriteLine($"Push bildirimi -> userId: {userId}, mesaj: {message}");
            await Task.CompletedTask;
        }
    }
}
