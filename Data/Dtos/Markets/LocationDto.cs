namespace Data.Dtos.Markets
{
    public class LocationDto
    {
    }

    // Ülke DTO'ları
    public class CountryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }

    }

    public class CountryCreateDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    // İl DTO'ları
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
    }

    // İlçe DTO'ları
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

    // Mahalle DTO'ları
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

}
