namespace Data.Dtos.Stores.Markets
{
    public class StoreMarketCountryCreateDto
    {
        public int StoreId { get; set; }
        public int CountryId { get; set; }
        public int DeliveryTimeFrame { get; set; }
    }

    public class StoreMarketCountryDto
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int DeliveryTimeFrame { get; set; }
        public bool IsActive { get; set; }
    }

    public class StoreMarketCountryUpdateDto
    {
        public int Id { get; set; }
        public int DeliveryTimeFrame { get; set; }
        public bool IsActive { get; set; }
    }

    public class StoreMarketProvinceCreateDto
    {
        public int StoreId { get; set; }
        public int ProvinceId { get; set; }
        public int DeliveryTimeFrame { get; set; }
    }

    public class StoreMarketProvinceDto
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public int DeliveryTimeFrame { get; set; }
        public bool IsActive { get; set; }
    }

    public class StoreMarketProvinceUpdateDto
    {
        public int Id { get; set; }
        public int DeliveryTimeFrame { get; set; }
        public bool IsActive { get; set; }
    }

    public class StoreMarketDistrictCreateDto
    {
        public int StoreId { get; set; }
        public int DistrictId { get; set; }
        public int DeliveryTimeFrame { get; set; }
    }

    public class StoreMarketDistrictDto
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int DeliveryTimeFrame { get; set; }
        public bool IsActive { get; set; }
    }

    public class StoreMarketDistrictUpdateDto
    {
        public int Id { get; set; }
        public int DeliveryTimeFrame { get; set; }
        public bool IsActive { get; set; }
    }

    public class StoreMarketNeighborhoodCreateDto
    {
        public int StoreId { get; set; }
        public int NeighborhoodId { get; set; }
        public int DeliveryTimeFrame { get; set; }
    }

    public class StoreMarketNeighborhoodDto
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int NeighborhoodId { get; set; }
        public string NeighborhoodName { get; set; }
        public int DeliveryTimeFrame { get; set; }
        public bool IsActive { get; set; }
    }

    public class StoreMarketNeighborhoodUpdateDto
    {
        public int Id { get; set; }
        public int DeliveryTimeFrame { get; set; }
        public bool IsActive { get; set; }
    }


    public class StoreMarketRegionCreateDto
    {
        public int StoreId { get; set; }
        public int RegionId { get; set; }
        public int DeliveryTimeFrame { get; set; }
    }

    public class StoreMarketRegionDto
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public int DeliveryTimeFrame { get; set; }
        public bool IsActive { get; set; }
    }

    public class StoreMarketRegionUpdateDto
    {
        public int Id { get; set; }
        public int DeliveryTimeFrame { get; set; }
        public bool IsActive { get; set; }
    }
    public class StoreMarketStateCreateDto
    {
        public int StoreId { get; set; }
        public int StateId { get; set; }
        public int DeliveryTimeFrame { get; set; }
    }

    public class StoreMarketStateDto
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int DeliveryTimeFrame { get; set; }
        public bool IsActive { get; set; }
    }

    public class StoreMarketStateUpdateDto
    {
        public int Id { get; set; }
        public int DeliveryTimeFrame { get; set; }
        public bool IsActive { get; set; }
    }

}
