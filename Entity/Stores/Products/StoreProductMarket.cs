namespace Entity.Stores.Products
{
    public class StoreProductMarket
    {
        public int Id { get; set; }

        public int StoreProductId { get; set; }
        public virtual StoreProduct StoreProduct { get; set; }
    }
}
