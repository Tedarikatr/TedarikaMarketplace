namespace Entity.Baskets
{
    public class BasketItem
    {
        public int Id { get; set; }

        public int BasketId { get; set; }
        public Basket Basket { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string StoreProductImageUrl { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; } 
        public decimal TotalPrice { get; set; } 
    }

}
