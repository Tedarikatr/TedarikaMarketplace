using Entity.Markets.Locations;

namespace Entity.Stores.Markets
{
    public class StoreMarketState
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }

        public int StateId { get; set; }
        public State State { get; set; }

        public int DeliveryTimeFrame { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
