using Entity.Products;
using Microsoft.AspNetCore.Http;

namespace Data.Dtos.Stores
{
    public class StoreProductDto
    {
    }
    public class StoreProductRequestCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }

        public UnitType UnitType { get; set; }
        public string Specifications { get; set; }

        public decimal Price { get; set; }
        public int MinOrderQuantity { get; set; }
        public int MaxOrderQuantity { get; set; }

        public IFormFile Image { get; set; }

        public bool AllowedDomestic { get; set; }
        public bool AllowedInternational { get; set; }

        public int? CategoryId { get; set; }
        public int? CategorySubId { get; set; }
    }

}
