namespace Data.Dtos.Availability
{
    public class StoreAvailabilityFilterDto
    {
        public string Region { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string Neighborhood { get; set; }
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
}
