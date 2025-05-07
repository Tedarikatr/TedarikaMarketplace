using Entity.Orders;

namespace Entity.Payments
{
    public class Payment
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public int OrderId { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus Status { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal PaidPrice { get; set; }
        public decimal PaidAmount { get; set; }
        public string Currency { get; set; } = "TRY";
        public string OrderNumber { get; set; } 

        public string? PaymentReference { get; set; } 
        public string? ErrorMessage { get; set; }
        public string? ErrorCode { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public enum PaymentMethod
    {
        CashOnDelivery = 1,
        CreditCard = 2,
        WireTransfer = 3,
        Iyzico = 4,
        Online = 5
    }

    public enum PaymentStatus
    {
        Pending = 1,
        Completed = 2,
        Failed = 3,
        Cancelled = 4
    }
}
