namespace Data.Dtos.Forms
{
    public class SellerApplicationCreateDto
    {
        public string StoreName { get; set; }
        public string BusinessType { get; set; }
        public string TaxNumber { get; set; }
        public string TaxOffice { get; set; }
        public string GTIPFocusArea { get; set; }

        public string RepresentativeFullName { get; set; }
        public string RepresentativePosition { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
    }

    public class SellerApplicationListDto
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public string BusinessType { get; set; }
        public string RepresentativeFullName { get; set; }
        public string Email { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class SellerApplicationDetailDto
    {
        public int Id { get; set; }
        public Guid GuidId { get; set; }

        public string StoreName { get; set; }
        public string BusinessType { get; set; }
        public string TaxNumber { get; set; }
        public string TaxOffice { get; set; }
        public string GTIPFocusArea { get; set; }

        public string RepresentativeFullName { get; set; }
        public string RepresentativePosition { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }

        public bool IsApproved { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class SellerApplicationUpdateApprovalDto
    {
        public int Id { get; set; }
        public bool IsApproved { get; set; }
        public string Notes { get; set; }
    }
}
