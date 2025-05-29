using Entity.Auths;
using Entity.Stores.Products;

namespace Entity.OrderOffers
{
    public class QuotationRequest
    {
        public int Id { get; set; }
        public int BuyerUserId { get; set; }
        public BuyerUser BuyerUser { get; set; }

        public int StoreProductId { get; set; }
        public StoreProduct StoreProduct { get; set; }

        public int Quantity { get; set; }
        public string Message { get; set; }
        public DateTime RequestedAt { get; set; }
        public QuotationRequestStatus Status { get; set; }

        public ICollection<QuotationResponse> Responses { get; set; }
    }

    public enum QuotationRequestStatus
    {
        Pending = 0,
        Responded = 1,
        Cancelled = 2
    }
}
