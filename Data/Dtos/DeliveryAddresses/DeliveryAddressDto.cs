namespace Data.Dtos.DeliveryAddresses
{
    public class DeliveryAddressDto
    {
        public int Id { get; set; }
        public int BuyerUserId { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string ProvinceName { get; set; }
        public string DistrictName { get; set; }
        public string NeighborhoodName { get; set; }
        public string AddressLine { get; set; }
        public string PostalCode { get; set; }
        public bool IsDefault { get; set; }
    }

    public class DeliveryAddressCreateDto
    {

        public int RegionId { get; set; }
        public int CountryId { get; set; }
        public int? StateId { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int NeighborhoodId { get; set; }
        public string AddressLine { get; set; }
        public string PostalCode { get; set; }
        public bool IsDefault { get; set; }
    }

    public class DeliveryAddressUpdateDto
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public int? StateId { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int NeighborhoodId { get; set; }
        public string AddressLine { get; set; }
        public string PostalCode { get; set; }
        public bool IsDefault { get; set; }
    }
}
