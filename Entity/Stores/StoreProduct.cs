using Entity.Products;

namespace Entity.Stores
{
    public class StoreProduct
    {
        public int StoreId { get; set; }
        public Store Store { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; }


        // Ürünün hangi pazarlara açık olduğu (Yurtiçi/Yurtdışı)
        public bool AllowedDomestic { get; set; }
        public bool AllowedInternational { get; set; }

        // Ürünün hangi ülke/şehirlere gönderilebileceği
        public virtual ICollection<ProductShippingRegion> ShippingRegions { get; set; }
    }
}
