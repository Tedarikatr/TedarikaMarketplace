namespace Data.Dtos.Stores
{
    public class StoreMarketDto
    {
        public int StoreId { get; set; }
        public int MarketId { get; set; } 
    }

    public class StoreMarketAddDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAlreadyAdded { get; set; }
        public bool? IsActive { get; set; }
    }
}
