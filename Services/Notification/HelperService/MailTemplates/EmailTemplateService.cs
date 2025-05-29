namespace Services.Notification.HelperService.MailTemplates
{
    public interface IEmailTemplateService
    {
        string GetWelcomeTemplate(string userName);
        string GetOrderCreatedTemplate(string userName, int orderId, decimal totalAmount, string date, string orderLink);
    }

    public class EmailTemplateService : IEmailTemplateService
    {
        public string GetWelcomeTemplate(string userName)
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

        public string GetOrderCreatedTemplate(string userName, int orderId, decimal totalAmount, string date, string orderLink)
        {
            return $@"
                <!DOCTYPE html>
                <html>
                <head>
                  <meta charset='UTF-8'>
                  <title>Siparişiniz Alındı</title>
                </head>
                <body style='font-family: Arial, sans-serif;'>
                  <img src='cid:logo' alt='Tedarika Logo' width='120'/>
                  <h2>Siparişiniz Başarıyla Oluşturuldu!</h2>
                  <p>Sayın <strong>{userName}</strong>,</p>
                  <p>Siparişiniz başarıyla alındı. Detaylar aşağıdadır:</p>
                  <ul>
                    <li><strong>Sipariş No:</strong> {orderId}</li>
                    <li><strong>Toplam Tutar:</strong> {totalAmount} ₺</li>
                    <li><strong>Tarih:</strong> {date}</li>
                  </ul>
                  <p>Siparişinizi <a href='{orderLink}'>buradan</a> takip edebilirsiniz.</p>
                  <p>Teşekkür ederiz!<br/>Tedarika Ekibi</p>
                </body>
                </html>";
        }
    }
}
