using Entity.Products;
using Microsoft.AspNetCore.Http;

namespace Data.Dtos.Stores
{
    public class StoreProductRequestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string ImageUrl { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
    }

    public class StoreProductRequestCreateDto
    {
        public int StoreId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string UnitTypes { get; set; }
        public UnitType UnitType { get; set; }
        public IFormFile Image { get; set; }

        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int MinOrderQuantity { get; set; }
        public int MaxOrderQuantity { get; set; }

        public bool AllowedDomestic { get; set; }
        public bool AllowedInternational { get; set; }
    }
}
