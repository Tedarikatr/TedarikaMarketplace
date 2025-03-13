using Entity.Stores;

namespace Entity.Auths
{
    public class SellerUser : UserBase
    {
        public virtual ICollection<Store> Store { get; set; }

    }
}
