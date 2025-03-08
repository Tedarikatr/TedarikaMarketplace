using Entity.Stores;

namespace Entity.Carrier
{
    public class Carrier
    {
        public int CarrierId { get; set; }
        public string Name { get; set; }
        public ICollection<StoreCarrier> StoreCarriers { get; set; }
    }
}
