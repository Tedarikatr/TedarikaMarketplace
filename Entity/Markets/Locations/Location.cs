namespace Entity.Markets.Locations
{
    public class Location
    {
        public int Id { get; set; }

        public int? CountryId { get; set; }
        public Country Country { get; set; }

        public int? ProvinceId { get; set; }
        public Province Province { get; set; }

        public int? DistrictId { get; set; }
        public District District { get; set; }

        public int? NeighborhoodId { get; set; }
        public Neighborhood Neighborhood { get; set; }

        public string FullAddress => $"{Neighborhood?.Name}, {District?.Name}, {Province?.Name}, {Country?.Name}";
        public string PostalCode => Neighborhood?.PostalCode;

        public ICollection<MarketAddressLocation> MarketAddressLocations { get; set; }

        public MarketCoverageLevel CoverageLevel { get; set; }

    }
}
