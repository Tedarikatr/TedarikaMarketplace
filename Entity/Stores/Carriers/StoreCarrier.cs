using Entity.Carriers;

namespace Entity.Stores.Carriers
{
    public class StoreCarrier
    {
        public int Id { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }

        public int CarrierId { get; set; }
        public Carrier Carrier { get; set; }

        public string? StoreApiKey { get; set; }
        public bool IsEnabled { get; set; } = true;
    }
}
