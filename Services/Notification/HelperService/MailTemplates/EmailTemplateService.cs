using System;
namespace Services.Notification.HelperService.MailTemplates
{
    public interface IEmailTemplateService
    {
        string GetWelcomeSellerTemplate(string userName);
        string GetWelcomeBuyerTemplate(string userName);
    }

    public class EmailTemplateService : IEmailTemplateService
    {
        private const string LogoCid = "logo";

        private string WrapWithLayout(string bodyContent, string title)
        {
            return $@"
<!DOCTYPE html>
<html lang='tr'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>{title}</title>
</head>
<body style='margin:0;padding:0;background-color:#f4f4f4;font-family:Arial,sans-serif;'>
    <table width='100%' cellpadding='0' cellspacing='0' border='0' style='background-color:#f4f4f4;padding:30px 0;'>
        <tr>
            <td align='center'>
                <table width='600' cellpadding='0' cellspacing='0' border='0' style='background-color:#ffffff;border-radius:8px;overflow:hidden;box-shadow:0 2px 10px rgba(0,0,0,0.1);'>
                    <tr style='background-color:#007bff;color:#ffffff;'>
                        <td style='padding:20px;text-align:center;'>
                            <img src='cid:{LogoCid}' alt='Tedarika' style='width:140px;height:auto;display:block;margin:0 auto;' />
                        </td>
                    </tr>
                    <tr>
                        <td style='padding:30px;'>
                            {bodyContent}
                        </td>
                    </tr>
                    <tr>
                        <td style='background-color:#f8f9fa;padding:20px;text-align:center;color:#888;font-size:12px;'>
                            Bu e-posta Tedarika tarafından otomatik olarak gönderilmiştir. Lütfen yanıtlamayınız.<br/>
                            &copy; {DateTime.UtcNow.Year} Tedarika Global Pazaryeri
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>";
        }

        public string GetWelcomeSellerTemplate(string userName)
        {
            var body = $@"
                <h2 style='color:#333;'>Tedarika’ya Hoş Geldiniz!</h2>
                <p>Merhaba <strong>{userName}</strong>,</p>
                <p>Tedarika platformuna katıldığınız için çok memnunuz. Global pazaryeri yolculuğunuzda size her adımda destek olacağız.</p>
                <p>Satıcı panelinize giriş yaparak ürünlerinizi yüklemeye başlayabilirsiniz:</p>
                <p style='margin:20px 0;'>
                    <a href='https://seller.tedarika.app' style='background-color:#007bff;color:#ffffff;padding:10px 20px;text-decoration:none;border-radius:4px;'>Satıcı Paneline Git</a>
                </p>
                <p>Başarılar dileriz!<br/>Tedarika Ekibi</p>";

            return WrapWithLayout(body, "Tedarika - Satıcı Hoşgeldiniz");
        }

        public string GetWelcomeBuyerTemplate(string userName)
        {
            var body = $@"
                <h2 style='color:#333;'>Tedarika’ya Hoş Geldiniz!</h2>
                <p>Merhaba <strong>{userName}</strong>,</p>
                <p>Tedarika ailesine katıldığınız için çok mutluyuz. İhtiyacınıza uygun tedarikçileri ve ürünleri keşfetmek için hazırsınız!</p>
                <p>Alıcı panelinize erişmek için aşağıdaki butonu kullanabilirsiniz:</p>
                <p style='margin:20px 0;'>
                    <a href='https://buyer.tedarika.app' style='background-color:#28a745;color:#ffffff;padding:10px 20px;text-decoration:none;border-radius:4px;'>Alıcı Paneline Git</a>
                </p>
                <p>İyi alışverişler dileriz!<br/>Tedarika Ekibi</p>";

            return WrapWithLayout(body, "Tedarika - Alıcı Hoşgeldiniz");
        }
    }
}
