namespace Services.Notification.HelperService.MailTemplates
{
    public interface IEmailTemplateService
    {
        string GetWelcomeSellerTemplate(string userName);
        string GetWelcomeBuyerTemplate(string userName);
    }

    public class EmailTemplateService : IEmailTemplateService
    {
        public string GetWelcomeSellerTemplate(string userName)
        {
            return $@"
                <!DOCTYPE html>
                <html>
                <head>
                  <meta charset='UTF-8'>
                  <title>Hoş Geldiniz</title>
                </head>
                <body style='font-family: Arial, sans-serif;'>
                  <img src='cid:logo' alt='Tedarika Logo' width='120'/>
                  <h2>Tedarika’ya Hoş Geldiniz!</h2>
                  <p>Merhaba <strong>{userName}</strong>,</p>
                  <p>Tedarika’ya katıldığınız için çok memnunuz. Global pazaryeri yolculuğunuzda size destek olacağız.</p>
                  <p>Başlamak için <a href='https://seller.tedarika.app'>panelinize giriş yapabilirsiniz</a>.</p>
                  <p>İyi alışverişler dileriz!<br/>Tedarika Ekibi</p>
                </body>
                </html>";
        }

        public string GetWelcomeBuyerTemplate(string userName)
        {
            return $@"
                    <!DOCTYPE html>
                    <html>
                    <head>
                        <meta charset='UTF-8'>
                        <title>Hoş Geldiniz</title>
                    </head>
                    <body style='font-family: Arial, sans-serif; background-color: #f5f5f5; padding: 20px;'>
                        <div style='max-width: 600px; margin: auto; background-color: #ffffff; padding: 30px; border-radius: 8px; box-shadow: 0 2px 5px rgba(0,0,0,0.1);'>
                            <div style='text-align: center;'>
                                <img src='cid:logo.png' alt='Tedarika' style='width: 150px; margin-bottom: 20px;' />
                            </div>
                            <h2 style='color: #333;'>Tedarika’ya Hoş Geldiniz!</h2>
                            <p>Merhaba <strong>{userName}</strong>,</p>
                            <p>Tedarika ailesine katıldığınız için çok memnunuz. Global pazaryeri yolculuğunuzda size destek olacağız.</p>
                            <p>Başlamak için <a href='https://buyer.tedarika.app' style='color: #007bff;'>alıcı panelinize giriş yapabilirsiniz</a>.</p>
                            <p>İyi alışverişler dileriz!<br/>Tedarika Ekibi</p>
                            <hr style='margin: 30px 0;'>
                            <small style='color: #999;'>Bu e-posta, Tedarika tarafından otomatik olarak gönderilmiştir. Lütfen yanıtlamayınız.</small>
                        </div>
                    </body>
                    </html>";
        }
    }
}
