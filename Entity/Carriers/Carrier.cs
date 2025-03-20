using Entity.Markets;

namespace Entity.Carriers
{
    public class Carrier
    {
        public int Id { get; set; }
        public string Name { get; set; }  

        public ICollection<MarketCarrier> MarketCarriers { get; set; }
    }
}
