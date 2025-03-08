using Entity.Stores;

namespace Entity.Products
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public bool AllowedDomestic { get; set; }
        public bool AllowedInternational { get; set; }
        public ICollection<StoreProduct> StoreProducts { get; set; }
    }
}
