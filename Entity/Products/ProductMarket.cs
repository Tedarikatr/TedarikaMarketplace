using Entity.Markets;

namespace Entity.Products
{
    public class ProductMarket
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int MarketId { get; set; }
        public virtual Market Market { get; set; }
    }
}
