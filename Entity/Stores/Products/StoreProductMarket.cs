using Entity.Markets;
using Entity.Products;

namespace Entity.Stores.Products
{
    public class StoreProductMarket
    {
        public int Id { get; set; }

        public int StoreProductId { get; set; }
        public virtual StoreProduct StoreProduct { get; set; }

        public int MarketId { get; set; }
        public virtual Market Market { get; set; }
    }
}
