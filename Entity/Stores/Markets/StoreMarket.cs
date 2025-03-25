using Entity.Markets;

namespace Entity.Stores.Market
{
    public class StoreMarket
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string RegionCode { get; set; }

        public bool IsActive { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }

        public int MarketId { get; set; }
        public Market Market { get; set; }
    }
}
