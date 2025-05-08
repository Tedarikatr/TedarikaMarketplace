namespace Data.Dtos.Availability
{
    public class StoreAvailabilityFilterDto
    {
        public int? RegionId { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public int? NeighborhoodId { get; set; }
    }

    public class AvailableStoreDto
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string CompanyName { get; set; }
        public List<AvailableStoreProductDto> Products { get; set; }
    }

    public class AvailableStoreProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class AvailableStoreWithProductsDto
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string CompanyName { get; set; }
        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }

        public List<AvailableStoreProductDto> Products { get; set; } = new();
    }
}
