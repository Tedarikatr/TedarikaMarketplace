using Entity.Incoterms;

namespace Entity.Stores.Products
{
    public class StoreProductIncoterm
    {
        public int Id { get; set; }

        public int StoreProductId { get; set; }
        public StoreProduct StoreProduct { get; set; }

        public int IncotermId { get; set; }
        public Incoterm Incoterm { get; set; }

        public decimal? EstimatedDeliveryCost { get; set; }
        public string Currency { get; set; } 

        public string DestinationCountryCode { get; set; } 

        public DateTime CreatedAt { get; set; }
    }
}
