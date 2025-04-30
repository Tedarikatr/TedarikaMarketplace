using Entity.Markets.Locations;

namespace Data.Dtos.Stores.Markets
{
    public class StoreMarketCoverageDto
    {
        public int Id { get; set; }
        public int? CountryId { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public int? NeighborhoodId { get; set; }
        public MarketCoverageLevel CoverageLevel { get; set; }
        public bool IsActive { get; set; }
    }

    public class StoreMarketCoverageCreateDto
    {
        public int? CountryId { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public int? NeighborhoodId { get; set; }
    }
}
