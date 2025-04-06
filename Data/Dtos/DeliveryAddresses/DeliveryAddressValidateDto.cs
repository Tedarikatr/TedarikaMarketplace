namespace Data.Dtos.DeliveryAddresses
{
    public class DeliveryAddressValidateDto
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
    }

    public class AddressValidationResultDto
    {
        public bool IsValid { get; set; }
        public string NormalizedAddress { get; set; }
        public string Message { get; set; }
    }
}
