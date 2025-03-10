using Entity.Stores;

namespace Entity.Products
{
    public class ProductShippingRegion
    {
        public int Id { get; set; }

        public int StoreProductId { get; set; }
        public virtual StoreProduct StoreProduct { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
    }
}
