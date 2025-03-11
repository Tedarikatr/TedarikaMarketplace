namespace Entity.Carriers
{
    public class Carrier
    {
        public int Id { get; set; }
        public string Name { get; set; }  // Örn: "DHL", "UPS", "MNG Kargo"

        public ICollection<MarketCarrier> MarketCarriers { get; set; }
    }
}
