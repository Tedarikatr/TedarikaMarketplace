using Services.Notification.HelperService;
using Services.Notification.HelperService.MailTemplates;
using Services.Notification.IServices;

namespace Services.Notification.Service
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly IPushSender _pushSender;
        private readonly IWebSocketSender _webSocketSender;
        private readonly IEmailTemplateService _emailTemplateService;


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

        public async Task WelcomeBuyerSendEmailAsync(string to, string userName)
        {
            var subject = "Tedarika'ya Hoş Geldiniz!";
            var body = _emailTemplateService.GetWelcomeBuyerTemplate(userName);
            await SafeSendEmailAsync(to, subject, body);
        }

        public async Task WelcomeSellerSendEmailAsync(string to, string userName)
        {
            var subject = "Tedarika'ya Hoş Geldiniz!";
            var body = _emailTemplateService.GetWelcomeSellerTemplate(userName);
            await SafeSendEmailAsync(to, subject, body);
        }

        private async Task SafeSendEmailAsync(string to, string subject, string body)
        {
            try
            {
                await _emailSender.SendEmailAsync(to, subject, body);
            }
            catch (Exception ex)
            {
                // Hata loglanır, ama sistem akışı etkilenmez
                Console.WriteLine($"[ERROR] Mail gönderilemedi: {ex.Message}");
            }
        }

    }
}
