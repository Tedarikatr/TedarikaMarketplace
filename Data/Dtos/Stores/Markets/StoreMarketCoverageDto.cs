using Entity.Markets.Locations;

namespace Data.Dtos.Stores.Markets
{
    public class StoreMarketCoverageDto
    {
        public int Id { get; set; }
        public MarketCoverageLevel CoverageLevel { get; set; }
        public int DeliveryTimeFrame { get; set; }
        public string CountryName { get; set; }
        public string ProvinceName { get; set; }
        public string DistrictName { get; set; }
        public string NeighborhoodName { get; set; }
        public string RegionName { get; set; }
        public bool IsActive { get; set; }
    }

    public class StoreMarketCoverageCreateDto
    {
        public MarketCoverageLevel CoverageLevel { get; set; }
        public int DeliveryTimeFrame { get; set; }
        public int? CountryId { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public int? NeighborhoodId { get; set; }
        public int? RegionId { get; set; }
    }

    public class StoreMarketCoverageUpdateDto
    {
        public int Id { get; set; }
        public int DeliveryTimeFrame { get; set; }
        public bool IsActive { get; set; }
    }

    public class StoreMarketCoverageBatchDto
    {
        public int StoreId { get; set; }
        public List<StoreMarketCoverageCreateDto> Coverages { get; set; }
    }

    public class StoreMarketCoverageMultiCreateDto
    {
        public int StoreId { get; set; }
        public MarketCoverageLevel CoverageLevel { get; set; }
        public int DeliveryTimeFrame { get; set; }

        public List<int> CountryIds { get; set; }
        public List<int> ProvinceIds { get; set; }
        public List<int> DistrictIds { get; set; }
        public List<int> NeighborhoodIds { get; set; }
    }


}
