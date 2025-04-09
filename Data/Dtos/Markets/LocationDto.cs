namespace Data.Dtos.Markets
{
    public class LocationDto
    {
    }
    public class CountryCreateDto { public string Name { get; set; } public string Code { get; set; } }
    public class ProvinceCreateDto { public int CountryId { get; set; } public string Name { get; set; } }
    public class DistrictCreateDto { public int ProvinceId { get; set; } public string Name { get; set; } }
    public class NeighborhoodCreateDto { public int DistrictId { get; set; } public string Name { get; set; } public string PostalCode { get; set; } }

}
