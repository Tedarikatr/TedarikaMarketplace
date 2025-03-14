using Entity.Companies;

namespace Entity.Auths
{
    public class BuyerUser : UserBase
    {
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
