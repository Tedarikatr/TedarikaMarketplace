namespace Data.Dtos.Stores.Locations
{
    public class IdNameDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class StoreCoverageHierarchyDto
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public List<IdNameDto> Regions { get; set; } = new();
        public List<IdNameDto> Countries { get; set; } = new();
    }

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
