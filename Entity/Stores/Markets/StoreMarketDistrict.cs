using Entity.Markets.Locations;

namespace Entity.Stores.Markets
{
    public class StoreMarketDistrict
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }

        public int DistrictId { get; set; }
        public District District { get; set; }

        // ➕ Üst ilişkiler
        public Province Province => District?.Province;
        public Country Country => District?.Province?.Country;
        public State State => District?.Province?.State;
        public Region Region => District?.Province?.Country?.Region;


        public int DeliveryTimeFrame { get; set; }
        public bool IsActive { get; set; } = true;
    }

}
