using Data.Dtos.Stores;
using Data.Dtos.Stores.Products;

namespace Data.Dtos.Availability
{
    public class StoreAvailabilityDto
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<StoreProductDto> Products { get; set; } = new();
    }
    public class AvailableStoreWithProductsDto
    {
        public StoreDto Store { get; set; }
        public List<StoreProductDto> Products { get; set; }
    }
}
