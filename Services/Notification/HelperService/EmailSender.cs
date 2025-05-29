using MimeKit;

namespace Services.Notification.HelperService
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string body);
    }

    public class EmailSender : IEmailSender
    {
        private readonly string _from = "info@tedarika.app";
        private readonly string _password = "fcbk zbmd kwwc ezlu"; 

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Tedarika", _from));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = body };
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new MailKit.Net.Smtp.SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_from, _password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
