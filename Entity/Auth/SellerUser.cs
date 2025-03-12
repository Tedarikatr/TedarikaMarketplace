using Entity.Companys;
using Entity.Stores;

namespace Entity.Auth
{
    public class SellerUser : UserBase
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string UserNumber { get; set; }
        public bool Status { get; set; }
        public Guid GuidNumber { get; set; }

        public virtual ICollection<Store> Store { get; set; }

        public ICollection<Company> Company { get; set; } 

    }
}
