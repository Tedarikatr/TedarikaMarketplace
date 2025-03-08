namespace Entity.Stores
{
    public class StoreMarket
    {
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public int MarketId { get; set; }
        public Market Market { get; set; }
    }
}
