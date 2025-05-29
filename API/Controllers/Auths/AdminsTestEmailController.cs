using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Notification.HelperService;

namespace API.Controllers.Auths
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "seller")]
    public class AdminsTestEmailController : ControllerBase
    {
        private readonly IEmailSender _emailSender;
        private readonly ILogger<AdminsTestEmailController> _logger;

        public AdminsTestEmailController(IEmailSender emailSender, ILogger<AdminsTestEmailController> logger)
        {
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
    }
}
