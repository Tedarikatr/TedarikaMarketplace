using Entity.Markets.Locations;

namespace Entity.Stores.Markets
{
    public class StoreMarketNeighborhood
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }

        public int NeighborhoodId { get; set; }
        public Neighborhood Neighborhood { get; set; }

        public int DeliveryTimeFrame { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
