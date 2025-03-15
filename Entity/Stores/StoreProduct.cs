using Entity.Products;

namespace Entity.Stores
{
    public class StoreProduct
    {
        public int Id { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; }

        public bool AllowedDomestic { get; set; }
        public bool AllowedInternational { get; set; }

        public virtual ICollection<StoreProductMarket> ProductMarkets { get; set; }
        public virtual ICollection<StoreProductShippingRegion> ShippingRegions { get; set; }
    }
}
