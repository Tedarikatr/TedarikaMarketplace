using Entity.Locations;

namespace Entity.Stores.Locations
{
    public class StoreLocationState
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }

        public int StateId { get; set; }
        public State State { get; set; }

        public string StateName { get; set; }
        public int DeliveryTimeFrame { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
