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

        public int DeliveryTimeFrame { get; set; }
        public bool IsActive { get; set; } = true;
    }

}
