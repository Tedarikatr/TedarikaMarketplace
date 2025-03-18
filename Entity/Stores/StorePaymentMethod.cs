using System.ComponentModel.DataAnnotations;

namespace Entity.Stores
{
    public class StorePaymentMethod
    {
        public int Id { get; set; }

        public int StoreId { get; set; }
        public virtual Store Store { get; set; }

        public PaymentMethodType PaymentMethod { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }

    public enum PaymentMethodType
    {
        CreditCard = 1,
        BankTransfer = 2,
        CashOnDelivery = 3,
        PayPal = 4,
        CryptoCurrency = 5
    }
}
