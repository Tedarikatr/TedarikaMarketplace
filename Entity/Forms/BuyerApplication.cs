namespace Entity.Forms
{
    public class BuyerApplication
    {
        public int Id { get; set; }

        public Guid GuidId { get; set; }

        public string ProductName { get; set; } 

        public int Quantity { get; set; }

        public string Unit { get; set; } 

        public DateTime NeededBy { get; set; }

        public string Country { get; set; }

        public string Province { get; set; }

        public string District { get; set; }

        public string AdditionalDetails { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsFulfilled { get; set; } = false;

        public string UserIpAddress { get; set; }

    }
}
