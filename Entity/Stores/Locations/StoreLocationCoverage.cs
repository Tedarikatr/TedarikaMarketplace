namespace Entity.Stores.Locations
{
    public class StoreLocationCoverage
    {
        public int Id { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }

        public List<int> RegionIds { get; set; } = new();
        public List<int> CountryIds { get; set; } = new();
        public List<int> StateIds { get; set; } = new();
        public List<int> ProvinceIds { get; set; } = new();
        public List<int> DistrictIds { get; set; } = new();
        public List<int> NeighborhoodIds { get; set; } = new();


        // Yeni eklenen alanlar - her seviye için ad bilgilerini de sakla
        public List<string> RegionNames { get; set; } = new();
        public List<string> CountryNames { get; set; } = new();
        public List<string> StateNames { get; set; } = new();
        public List<string> ProvinceNames { get; set; } = new();
        public List<string> DistrictNames { get; set; } = new();
        public List<string> NeighborhoodNames { get; set; } = new();


        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
