using Entity.Markets.Locations;

namespace Entity.Stores.Markets
{
    public class StoreMarketCountry
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public Region Region => Country?.Region;

        public string CountryName { get; set; } 
        public int DeliveryTimeFrame { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
