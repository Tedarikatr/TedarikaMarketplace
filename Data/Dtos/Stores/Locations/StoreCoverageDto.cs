namespace Data.Dtos.Stores.Locations
{
    public class StoreCoverageDto
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public List<int> RegionIds { get; set; } = new();
        public List<int> CountryIds { get; set; } = new();
    }

    public class StoreCoverageCreateDto
    {
        public List<int> RegionIds { get; set; } = new();
        public List<int> CountryIds { get; set; } = new();
    }

    public class StoreCoverageDeleteDto
    {
        public List<int> RegionIds { get; set; } = new();
        public List<int> CountryIds { get; set; } = new();
    }
}
