using Entity.Markets.Locations;

namespace Entity.Stores.Markets
{
    public class StoreMarketRegion
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }

        public int RegionId { get; set; }
        public Region Region { get; set; }

        public string RegionName { get; set; }
        public int DeliveryTimeFrame { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
