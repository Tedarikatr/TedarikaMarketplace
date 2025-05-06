using Entity.Products;

namespace Entity.Stores.Products
{
    public class StoreProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Specifications { get; set; }
        public string Brand { get; set; }
        public string ProductNumber { get; set; }

        public int UnitTypes { get; set; }
        public UnitType UnitType { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }
        public int MinOrderQuantity { get; set; }
        public int MaxOrderQuantity { get; set; }

        public bool IsActive { get; set; }
        public bool IsOnSale { get; set; }
        public bool AllowedDomestic { get; set; }
        public bool AllowedInternational { get; set; }

        public string ImageUrl { get; set; }
        public string StoreImageUrl { get; set; }


        public string CategoryName { get; set; }
        public string CategorySubName { get; set; }

        public virtual ICollection<StoreProductMarket> ProductMarkets { get; set; }
        public virtual ICollection<StoreProductShippingRegion> ShippingRegions { get; set; }
    }
}
