using Entity.Auths;

namespace Entity.Companies
{
    public class CompanyUser
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public int UserId { get; set; }
        public UserType UserType { get; set; }

    }
}
