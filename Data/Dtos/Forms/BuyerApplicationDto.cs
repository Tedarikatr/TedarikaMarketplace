namespace Data.Dtos.Forms
{

    public class BuyerApplicationCreateDto
    {
        public Guid BuyerUserId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public DateTime NeededBy { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string AdditionalDetails { get; set; }
    }

    public class BuyerApplicationDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public DateTime NeededBy { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string AdditionalDetails { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsFulfilled { get; set; }
        public string UserIpAddress { get; set; }
    }
}
