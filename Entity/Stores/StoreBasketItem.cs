namespace Entity.Stores
{
    public class StoreBasketItem
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public int ShopDirectId { get; set; }
        public int ShopDirectProductId { get; set; }
        public int Quantity { get; set; }

        public string ImageUrl { get; set; }
        public string ShopDirectProductName { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
