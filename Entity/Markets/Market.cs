using Entity.Markets.Locations;
using Entity.Stores.Markets;

namespace Entity.Markets
{
    public class Market
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string RegionCode { get; set; } 
        public MarketType MarketType { get; set; }

        public int DeliveryTimeFrame { get; set; }

        public ICollection<StoreMarket> StoreMarkets { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
