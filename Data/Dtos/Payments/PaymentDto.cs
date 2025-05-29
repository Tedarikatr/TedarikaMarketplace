using Entity.Payments;

namespace Data.Dtos.Payments
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus Status { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public string Currency { get; set; }

        public string? PaymentReference { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class PaymentCreateDto
    {
        public PaymentMethod PaymentMethod { get; set; }

        public decimal TotalAmount { get; set; }
        public string Currency { get; set; } = "TRY";

        public string? CardHolderName { get; set; }  
        public string? CardNumber { get; set; }         
        public string? ExpiryDate { get; set; }       
        public string? Cvc { get; set; }              

    }
}
