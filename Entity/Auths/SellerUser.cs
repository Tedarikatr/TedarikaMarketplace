using Entity.Companies;
using Entity.Stores;

namespace Entity.Auths
{
    public class SellerUser : UserBase
    {
        public int? CompanyId { get; set; }
        public Company Company { get; set; }

        public virtual ICollection<Store> Store { get; set; }

    }
}
