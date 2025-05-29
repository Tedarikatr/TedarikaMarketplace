using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Notification.HelperService;
using Services.Notification.IServices;

namespace API.Controllers.Auths
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "seller")]
    public class AdminsTestEmailController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<AdminsTestEmailController> _logger;

        public AdminsTestEmailController(INotificationService notificationService, IEmailSender emailSender, ILogger<AdminsTestEmailController> logger)
        {
            _notificationService = notificationService;
            _emailSender = emailSender;
            _logger = logger;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendTestEmail([FromQuery] string to)
        {
            try
            {
                var subject = "Tedarika Test Mail";
                var body = "<p>Bu bir test e-postasıdır. Sistem çalışıyor. birde unutmadan ben hilali çok seviyorummm</p>";

                await _emailSender.SendEmailAsync(to, subject, body);
                _logger.LogInformation("Test e-postası gönderildi: {To}", to);

                return Ok($"E-posta {to} adresine gönderildi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Test e-postası gönderimi sırasında hata oluştu.");
                return StatusCode(500, "E-posta gönderilemedi.");
            }
        }

        [HttpPost("seller")]
        public async Task<IActionResult> TestSellerEmail([FromQuery] string email, [FromQuery] string name = "Yiğit Satıcı")
        {
            try
            {
                await _notificationService.WelcomeSellerSendEmailAsync(email, name);
                _logger.LogInformation("Satıcı hoşgeldin e-postası gönderildi: {Email}", email);
                return Ok("Satıcı e-postası gönderildi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satıcı e-postası gönderilemedi.");
                return StatusCode(500, "Hata oluştu.");
            }
        }

        [HttpPost("buyer")]
        public async Task<IActionResult> TestBuyerEmail([FromQuery] string email, [FromQuery] string name = "Yiğit Alıcı")
        {
            try
            {
                await _notificationService.WelcomeBuyerSendEmailAsync(email, name);
                _logger.LogInformation("Alıcı hoşgeldin e-postası gönderildi: {Email}", email);
                return Ok("Alıcı e-postası gönderildi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Alıcı e-postası gönderilemedi.");
                return StatusCode(500, "Hata oluştu.");
            }
        }
    }
}
