using Entity.Auths;
using Entity.Stores;

namespace Entity.Companies
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string TaxNumber { get; set; }
        public string TaxOffice { get; set; }
        public string TaxDocument { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CompanyNumber { get; set; }

        public string Industry { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }
        public bool BuyerAccount { get; set; }
        public bool SellerAccount { get; set; }

        public CompanyType Type { get; set; }


        public ICollection<Store> Stores { get; set; }


        public int? BuyerUserId { get; set; }
        public BuyerUser BuyerUser { get; set; }

        public int? SellerUserId { get; set; }
        public SellerUser SellerUser { get; set; }
    }

    public enum CompanyType
    {
        Buyer = 1,
        Seller = 2,
        Both = 3
    }
}
