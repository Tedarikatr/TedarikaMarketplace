namespace Entity.Stores.Locations
{
    public class StoreLocationCoverage
    {
        public int Id { get; set; }
        public int StoreId { get; set; }

        public HashSet<int> RegionIds { get; set; } = new();
        public HashSet<int> CountryIds { get; set; } = new();
        public HashSet<int> StateIds { get; set; } = new();
        public HashSet<int> ProvinceIds { get; set; } = new();
        public HashSet<int> DistrictIds { get; set; } = new();
        public HashSet<int> NeighborhoodIds { get; set; } = new();

        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
