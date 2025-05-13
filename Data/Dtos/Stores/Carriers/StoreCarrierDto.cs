namespace Data.Dtos.Stores.Carriers
{
    public class StoreCarrierDto
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int CarrierId { get; set; }
        public string CarrierName { get; set; }
        public string CarrierLogoUrl { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class StoreCarrierCreateDto
    {
        public int StoreId { get; set; }
        public int CarrierId { get; set; }
        public string? StoreApiKey { get; set; }
    }

    public class StoreCarrierUpdateStatusDto
    {
        public int StoreCarrierId { get; set; }
        public bool IsEnabled { get; set; }
    }
}
