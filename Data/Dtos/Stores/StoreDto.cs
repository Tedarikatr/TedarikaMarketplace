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
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string AccountingIntegration { get; set; }
    }

    public class StoreUpdateDto
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string AccountingIntegration { get; set; }
    }

    public class StoreStatusDto
    {
        public int StoreId { get; set; }
        public bool IsActive { get; set; }
    }

    public class StorePaymentMethodDto
    {
        public int StoreId { get; set; }
        public string PaymentMethod { get; set; } // "CreditCard", "Cash", "BankTransfer" gibi değerler alabilir
    }

    public class StoreDeliveryOptionDto
    {
        public int StoreId { get; set; }
        public string DeliveryOption { get; set; } // "Standard", "Express", "SameDay" gibi değerler alabilir
        public int EstimatedDeliveryDays { get; set; }
    }
    public class StoreMarketDto
    {
        public int StoreId { get; set; }
        public int MarketId { get; set; } // Market tablosundaki ID ile eşleşecek
    }
}
