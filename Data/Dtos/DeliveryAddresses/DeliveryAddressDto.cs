namespace Data.Dtos.DeliveryAddresses
{
    public class DeliveryAddressDto
    {
        public int Id { get; set; }
        public int BuyerUserId { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Neighborhood { get; set; }
        public string State { get; set; } 
        public string PostalCode { get; set; }
        public bool IsDefault { get; set; }
    }

    public class DeliveryAddressUpdateDto
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Neighborhood { get; set; }
        public string State { get; set; }
        public string AddressLine { get; set; }
        public string PostalCode { get; set; }
        public bool IsDefault { get; set; }
    }

    public class DeliveryAddressCreateDto
    {
        public int BuyerUserId { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Neighborhood { get; set; }
        public string State { get; set; }
        public string AddressLine { get; set; }
        public string PostalCode { get; set; }
        public bool IsDefault { get; set; }
    }
}
