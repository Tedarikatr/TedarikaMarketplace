namespace Data.Dtos.Markets
{
    public class RegionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
    }

    public class CountryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }

    }

    public class CountryCreateDto
    {
        public int RegionId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class ProvinceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public bool IsActive { get; set; }
    }

    public class ProvinceCreateDto
    {
        public string Name { get; set; }
        public int CountryId { get; set; }
        public int? StateId { get; set; } 
    }

    public class DistrictDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProvinceId { get; set; }
        public bool IsActive { get; set; }

    }

    public class DistrictCreateDto
    {
        public string Name { get; set; }
        public string ProvinceName { get; set; }
        public int ProvinceId { get; set; }

    }

    public class NeighborhoodDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public int DistrictId { get; set; }
        public bool IsActive { get; set; }

    }

    public class NeighborhoodCreateDto
    {
        public string Name { get; set; }
        public string DistrictName { get; set; }
        public string PostalCode { get; set; }
        public int DistrictId { get; set; }

    }

    public class StateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }

    public class StateCreateDto
    {
        public string Name { get; set; }
        public int CountryId { get; set; }
    }

    public class MarketLocationHierarchyDto
    {
        public List<RegionDto> Regions { get; set; }
        public List<CountryDto> Countries { get; set; }
        public List<StateDto> States { get; set; }
        public List<ProvinceDto> Provinces { get; set; }
        public List<DistrictDto> Districts { get; set; }
        public List<NeighborhoodDto> Neighborhoods { get; set; }
    }

}
