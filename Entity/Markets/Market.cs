namespace Entity.Markets
{
    public class Market
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string RegionCode { get; set; } 

        public ICollection<MarketCarrier> MarketCarriers { get; set; }
    }
}
