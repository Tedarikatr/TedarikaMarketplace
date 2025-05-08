using Entity.Locations;

namespace Entity.Stores.Markets
{
    public class StoreMarketNeighborhood
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }

        public int NeighborhoodId { get; set; }
        public Neighborhood Neighborhood { get; set; }

        // ➕ Üst ilişkiler
        public District District => Neighborhood?.District;
        public Province Province => Neighborhood?.District?.Province;
        public Country Country => Neighborhood?.District?.Province?.Country;
        public State State => Neighborhood?.District?.Province?.State;
        public Region Region => Neighborhood?.District?.Province?.Country?.Region;

        public string NeighborhoodName { get; set; }
        public int DeliveryTimeFrame { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
