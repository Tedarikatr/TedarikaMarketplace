namespace Data.Dtos.Stores
{
    public class StoreDto
    {
    }

    public class StoreCreateDto
    {
        public string StoreName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }

    public class StoreUpdateDto
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }

    public class StoreStatusDto
    {
        public int StoreId { get; set; }
        public bool IsActive { get; set; }
    }

    public class StorePaymentMethodDto
    {
        public int StoreId { get; set; }
        public string PaymentMethod { get; set; } 
    }

    public class StoreDeliveryOptionDto
    {
        public int StoreId { get; set; }
        public string DeliveryOption { get; set; } 
        public int EstimatedDeliveryDays { get; set; }
    }
}
