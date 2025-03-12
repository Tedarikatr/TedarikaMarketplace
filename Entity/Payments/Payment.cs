using Entity.Orders;

namespace Entity.Payments
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentStatus Status { get; set; }
    }

    public enum PaymentMethod
    {
        CreditCard,
        BankTransfer,
        CashOnDelivery
    }

    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed
    }
}
