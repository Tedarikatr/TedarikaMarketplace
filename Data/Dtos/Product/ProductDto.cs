using Microsoft.AspNetCore.Http;

namespace Data.Dtos.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UnitTypes { get; set; }
        public string ProductNumber { get; set; }
        public string Barcode { get; set; }
        public string Brand { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public DateTime? PreparationTime { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int? CategorySubId { get; set; }
        public string CategorySubName { get; set; }
    }

    public class ProductCreateDto
    {
        public string Name { get; set; }
        public string UnitTypes { get; set; }
        public string ProductNumber { get; set; }
        public string Barcode { get; set; }
        public string Brand { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public DateTime? PreparationTime { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public IFormFile Image { get; set; }

        public int? CategoryId { get; set; }
        public int? CategorySubId { get; set; }
    }

    public class ProductUpdateDto : ProductCreateDto
    {
    }

    public class ProductChangeLogDto
    {
        public int ProductId { get; set; }
        public string ChangedField { get; set; }      
        public string OldValue { get; set; }      
        public string NewValue { get; set; }        
        public DateTime ChangedAt { get; set; }   
        public string ChangedBy { get; set; }       
    }
}
