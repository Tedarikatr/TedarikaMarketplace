using Entity.Categorys;
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



        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UnitTypes { get; set; }
        public UnitType UnitType { get; set; }

        public string ProductNumber { get; set; }
        public string Barcode { get; set; }
        public string Brand { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? PreparationTime { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public string ImageUrl { get; set; }

        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Category Category { get; set; }

        public int? CategorySubId { get; set; }
        public string CategorySubName { get; set; }
        public CategorySub CategorySub { get; set; }
    }
}
