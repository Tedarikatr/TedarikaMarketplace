namespace Data.Dtos.Stores.Products
{
    public class StoreProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public int UnitTypes { get; set; }
        public string ProductNumber { get; set; }
        public decimal UnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public int MinOrderQuantity { get; set; }
        public int MaxOrderQuantity { get; set; }
        public string CategoryName { get; set; }
        public string CategorySubName { get; set; }
        public bool IsOnSale { get; set; }
        public bool IsActive { get; set; }
        public string ImageUrl { get; set; }
        public string StoreProductImageUrl { get; set; }
    }

    public class StoreProductListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UnitTypes { get; set; }
        public decimal UnitPrice { get; set; }
        public string ImageUrl { get; set; }
        public string Brand { get; set; }
        public string StoreProductImageUrl { get; set; }
    }
    public class SetActiveStatusRequest
    {
        public int ProductId { get; set; }
        public bool IsActive { get; set; }
    }

    public class SetOnSaleStatusRequest
    {
        public int ProductId { get; set; }
        public bool IsOnSale { get; set; }
    }


}
