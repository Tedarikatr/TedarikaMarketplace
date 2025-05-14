using Entity.Categories;

namespace Entity.Products
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public string UnitTypes { get; set; }
        public UnitType UnitType { get; set; }

        public string ProductNumber { get; set; }
        public string Barcode { get; set; }
        public string Brand { get; set; }
        public string ImageUrl { get; set; }

        public string GTIPCode { get; set; }

        public ICollection<ProductExportRestriction> ProductExportRestrictions { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? PreparationTime { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Category Category { get; set; }

        public int? CategorySubId { get; set; }
        public string CategorySubName { get; set; }
        public CategorySub CategorySub { get; set; }
    }
}
