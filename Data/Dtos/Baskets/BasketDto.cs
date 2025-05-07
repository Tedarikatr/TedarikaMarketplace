namespace Data.Dtos.Baskets
{
    public class BasketDto
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public decimal TotalAmount { get; set; }

        public List<BasketItemDto> Items { get; set; }
    }

    public class BasketItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public decimal Total => UnitPrice * Quantity;
    }

    public class AddToBasketDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
