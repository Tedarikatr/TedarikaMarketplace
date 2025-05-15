using Entity.Products;
using Entity.Stores.Products;
using Microsoft.AspNetCore.Http;

namespace Data.Dtos.Stores.Products
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
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string UnitTypes { get; set; }
        public UnitType UnitType { get; set; }
        public IFormFile Image { get; set; }
        public bool AllowedDomestic { get; set; }
        public bool AllowedInternational { get; set; }

        public int? CategoryId { get; set; }    
        public int? CategorySubId { get; set; }
    }

    public class StoreProductRequestDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string ImageUrl { get; set; }
        public string UnitTypes { get; set; }

        public bool AllowedDomestic { get; set; }
        public bool AllowedInternational { get; set; }

        public int? CategoryId { get; set; }
        public int? CategorySubId { get; set; }
        public string CategoryName { get; set; }
        public string CategorySubName { get; set; }

        public StoreProductRequestStatus Status { get; set; }
        public string AdminNote { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public DateTime? ReviewedAt { get; set; }
    }

    public class StoreProductRequestUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string UnitTypes { get; set; }
        public IFormFile? Image { get; set; } 
        public bool AllowedDomestic { get; set; }
        public bool AllowedInternational { get; set; }
    }

    public class RequestSummaryDto
    {
        public int Total { get; set; }
        public int Approved { get; set; }
        public int Pending { get; set; }
        public int Rejected { get; set; }
    }
}
