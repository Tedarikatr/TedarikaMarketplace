using Entity.Markets.Locations;

namespace Entity.Stores.Markets
{
    public class StoreMarketRegion
    {
        public int Id { get; set; }

        public int StoreMarketId { get; set; }
        public StoreMarket StoreMarket { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }

        public bool IsActive { get; set; }
    }
}
