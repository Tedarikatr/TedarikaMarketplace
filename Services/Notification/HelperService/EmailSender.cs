namespace Services.Notification.HelperService
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string body);
    }

    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            Console.WriteLine($"Email gönderildi -> {to} - {subject} - {body}");
            await Task.CompletedTask;
        }
    }
}
