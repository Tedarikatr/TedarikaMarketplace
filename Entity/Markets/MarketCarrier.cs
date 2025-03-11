using Entity.Carriers;

namespace Entity.Markets
{
    public class MarketCarrier
    {
        public int MarketId { get; set; }
        public virtual Market Market { get; set; }

        public int CarrierId { get; set; }
        public virtual Carrier Carrier { get; set; }
    }
}
