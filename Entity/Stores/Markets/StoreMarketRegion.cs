namespace Entity.Stores.Markets
{
    public class StoreMarketRegion
    {
        public int Id { get; set; }

        public string Country { get; set; } 
        public string Province { get; set; } 
        public string District { get; set; } 

        public int StoreId { get; set; }
        public Store Store { get; set; }

        public bool IsCountryWide => !string.IsNullOrWhiteSpace(Country) && string.IsNullOrWhiteSpace(Province);
        public bool IsProvinceWide => !string.IsNullOrWhiteSpace(Province) && string.IsNullOrWhiteSpace(District);
        public bool IsDistrictOnly => !string.IsNullOrWhiteSpace(District);
    }
}
