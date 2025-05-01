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


    public class StoreMarketCountryMultiCreateDto
    {
        public int StoreId { get; set; }
        public List<int> CountryIds { get; set; }
        public int DeliveryTimeFrame { get; set; }
    }

    public class StoreMarketProvinceMultiCreateDto
    {
        public int StoreId { get; set; }
        public List<int> ProvinceIds { get; set; }
        public int DeliveryTimeFrame { get; set; }
    }

    public class StoreMarketDistrictMultiCreateDto
    {
        public int StoreId { get; set; }
        public List<int> DistrictIds { get; set; }
        public int DeliveryTimeFrame { get; set; }
    }

    public class StoreMarketNeighborhoodMultiCreateDto
    {
        public int StoreId { get; set; }
        public List<int> NeighborhoodIds { get; set; }
        public int DeliveryTimeFrame { get; set; }
    }

    public class StoreMarketRegionMultiCreateDto
    {
        public int StoreId { get; set; }
        public List<int> RegionIds { get; set; }
        public int DeliveryTimeFrame { get; set; }
    }

    public class StoreMarketStateMultiCreateDto
    {
        public int StoreId { get; set; }
        public List<int> StateIds { get; set; }
        public int DeliveryTimeFrame { get; set; }
    }

    public class StoreMarketCountryCascadeCreateDto
    {
        public int StoreId { get; set; }
        public int CountryId { get; set; }
        public int DeliveryTimeFrame { get; set; }

        public bool IncludeProvinces { get; set; } = true;
        public bool IncludeDistricts { get; set; } = true;
        public bool IncludeNeighborhoods { get; set; } = false; // opsiyonel
    }

    public class StoreMarketCoverageCompositeCreateDto
    {
        public int StoreId { get; set; }
        public int DeliveryTimeFrame { get; set; }

        public List<int> CountryIds { get; set; } = new();
        public List<int> ProvinceIds { get; set; } = new();
        public List<int> DistrictIds { get; set; } = new();
        public List<int> NeighborhoodIds { get; set; } = new();
        public List<int> StateIds { get; set; } = new();
        public List<int> RegionIds { get; set; } = new();

        public bool CascadeProvinceFromCountry { get; set; } = false;
        public bool CascadeDistrictFromProvince { get; set; } = false;
        public bool CascadeNeighborhoodFromDistrict { get; set; } = false;
    }

    public class StoreMarketCoverageCompositeDeleteDto
    {
        public int StoreId { get; set; }

        public List<int> CountryIds { get; set; } = new();
        public List<int> ProvinceIds { get; set; } = new();
        public List<int> DistrictIds { get; set; } = new();
        public List<int> NeighborhoodIds { get; set; } = new();
        public List<int> StateIds { get; set; } = new();
        public List<int> RegionIds { get; set; } = new();
    }

    public class StoreMarketCoverageHierarchyDto
    {
        public List<StoreMarketCountryDto> Countries { get; set; } = new();
        public List<StoreMarketProvinceDto> Provinces { get; set; } = new();
        public List<StoreMarketDistrictDto> Districts { get; set; } = new();
        public List<StoreMarketNeighborhoodDto> Neighborhoods { get; set; } = new();
        public List<StoreMarketStateDto> States { get; set; } = new();
        public List<StoreMarketRegionDto> Regions { get; set; } = new();
    }

    public class StoreMarketCoverageUpdateBaseDto
    {
        public int Id { get; set; }
        public int DeliveryTimeFrame { get; set; }
        public bool IsActive { get; set; }
    }
    public enum CoverageType
    {
        Country,
        Province,
        District,
        Neighborhood,
        Region,
        State
    }


}
