using Entity.Markets.Locations;

namespace Entity.Stores.Markets
{
    public class StoreMarketCoverage
    {
        public int Id { get; set; }

        public MarketCoverageLevel CoverageLevel { get; set; }

        public int StoreMarketId { get; set; }
        public StoreMarket StoreMarket { get; set; }

        public int? CountryId { get; set; }
        public Country Country { get; set; }

        public int? ProvinceId { get; set; }
        public Province Province { get; set; }

        public int? DistrictId { get; set; }
        public District District { get; set; }

        public int? NeighborhoodId { get; set; }
        public Neighborhood Neighborhood { get; set; }

        public bool IsActive { get; set; }
    }
}
