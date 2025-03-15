namespace Data.Dtos.Companys
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TaxNumber { get; set; }
        public string TaxOffice { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CompanyNumber { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }
        public bool BuyerAccount { get; set; }
        public bool SellerAccount { get; set; }
        public string Industry { get; set; }
    }

    public class CompanyCreateDto
    {
        public string Name { get; set; }
        public string TaxNumber { get; set; }
        public string TaxOffice { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CompanyNumber { get; set; }
        public string Industry { get; set; }
        public bool BuyerAccount { get; set; }
        public bool SellerAccount { get; set; }
    }

    public class CompanyUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TaxNumber { get; set; }
        public string TaxOffice { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Industry { get; set; }
    }

    public class CompanyStatusDto
    {
        public int CompanyId { get; set; }
        public bool IsActive { get; set; }
    }

    public class CompanyVerifyDto
    {
        public int CompanyId { get; set; }
        public bool IsVerified { get; set; }
    }
}
