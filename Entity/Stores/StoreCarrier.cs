using Entity.Carriers;

namespace Entity.Stores
{
    public class StoreCarrier
    {
        public int Id { get; set; }
        public Store Store { get; set; }
        public int CarrierId { get; set; }
        public Carrier Carrier { get; set; }
    }
}
