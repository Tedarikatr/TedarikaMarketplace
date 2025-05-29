using MailKit.Net.Smtp;
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


        public async Task SendEmailAsync(string to, string subject, string bodyHtml)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Tedarika", _from));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;

            var builder = new BodyBuilder();

            var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "tedarika.png");
            if (File.Exists(logoPath))
            {
                var logo = builder.LinkedResources.Add(logoPath);
                logo.ContentId = "logo"; 
            }

            builder.HtmlBody = bodyHtml;
            message.Body = builder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_from, _password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
