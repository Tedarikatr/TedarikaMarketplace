namespace Entity.Markets
{
    public class Market
    {
        public int Id { get; set; }
        public string Name { get; set; }  // Örn: "Güney Avrupa", "Orta Doğu"
        public string RegionCode { get; set; } // "EU_SOUTH", "ME" vb.

        public ICollection<MarketCarrier> MarketCarriers { get; set; }
    }
}
