namespace Data.Dtos.Carriers
{
    public class CarrierDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CarrierCode { get; set; }
        public string ApiEndpoint { get; set; }
        public string CarrierLogoUrl { get; set; }
        public bool IsActive { get; set; }
        public string IntegrationType { get; set; }
    }

    public class CarrierCreateDto
    {
        public string Name { get; set; }
        public string CarrierCode { get; set; }
        public string ApiEndpoint { get; set; }
        public string ApiKey { get; set; }
        public string CarrierLogoUrl { get; set; }
        public int IntegrationType { get; set; }
    }
}
