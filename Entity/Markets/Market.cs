namespace Entity.Markets
{
    public class Market
    {
        public int MarketId { get; set; }
        public string Name { get; set; }  // Örn: "Güney Avrupa", "Orta Doğu"
        public string RegionCode { get; set; } // "EU_SOUTH", "ME" vb.

        public ICollection<ProductMarket> ProductMarkets { get; set; }
        public ICollection<MarketCarrier> MarketCarriers { get; set; }
    }
}
