using Entity.Markets;

namespace Entity.Stores.Products
{
    public class StoreProductShippingRegion
    {
        public int Id { get; set; }

        public int StoreProductId { get; set; }
        public virtual StoreProduct StoreProduct { get; set; }

        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        public int EstimatedDeliveryDays { get; set; }
        public string AllowedCarriers { get; set; }

    }
}
