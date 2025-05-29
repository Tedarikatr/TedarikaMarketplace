using Entity.Auths;

namespace Entity.OrderOffers
{
    public class QuotationResponse
    {
        public int Id { get; set; }

        public int QuotationRequestId { get; set; }
        public QuotationRequest QuotationRequest { get; set; }

        public int SellerUserId { get; set; }
        public SellerUser SellerUser { get; set; }

        public decimal OfferedUnitPrice { get; set; }
        public int MinOrderQuantity { get; set; }
        public DateTime ValidUntil { get; set; }

        public string Notes { get; set; }
        public DateTime RespondedAt { get; set; }
    }
}
