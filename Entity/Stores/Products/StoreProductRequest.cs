using Entity.Products;

namespace Entity.Stores.Product
{
    public class StoreProductRequest
    {
        public int Id { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }

        public UnitType UnitType { get; set; }
        public string Specifications { get; set; }

        public decimal Price { get; set; }
        public int MinOrderQuantity { get; set; }
        public int MaxOrderQuantity { get; set; }

        public string ImageUrl { get; set; }

        public bool AllowedDomestic { get; set; }
        public bool AllowedInternational { get; set; }

        public int? CategoryId { get; set; }
        public int? CategorySubId { get; set; }

        public StoreProductRequestStatus Status { get; set; }
        public string? AdminNote { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? ReviewedAt { get; set; }
    }

    public enum StoreProductRequestStatus
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2
    }
}
