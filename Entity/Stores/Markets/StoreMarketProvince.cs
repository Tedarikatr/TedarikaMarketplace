using Entity.Markets.Locations;

namespace Entity.Stores.Markets
{
    public class StoreMarketProvince
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }

        public int ProvinceId { get; set; }
        public Province Province { get; set; }

        // ➕ Üst ilişkiler
        public Country Country => Province?.Country;
        public Region Region => Province?.Country?.Region;
        public State State => Province?.State;

        public string ProvinceName { get; set; }
        public int DeliveryTimeFrame { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
