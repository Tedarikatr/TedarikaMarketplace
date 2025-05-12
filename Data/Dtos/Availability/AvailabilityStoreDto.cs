using Data.Dtos.Stores.Products;

namespace Data.Dtos.Availability
{
    public class AvailableStoreDto
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string StoreDescription { get; set; }
        public string LogoUrl { get; set; }
        public int DeliveryTimeFrame { get; set; }

        public int? RegionId { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public int? NeighborhoodId { get; set; }
    }

    public class AvailableStoreWithProductsDto : AvailableStoreDto
    {
        public List<StoreProductListDto> Products { get; set; } = new();
    }
}
