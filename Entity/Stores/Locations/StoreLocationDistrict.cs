using Entity.Locations;

namespace Entity.Stores.Locations
{
    public class StoreLocationDistrict
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

        public string DistrictName { get; set; }
        public int DeliveryTimeFrame { get; set; }
        public bool IsActive { get; set; } = true;
    }

}
