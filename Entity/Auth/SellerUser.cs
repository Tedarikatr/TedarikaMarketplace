using Entity.Stores;

namespace Entity.Auth
{
    public class SellerUser : UserBase
    {
        public virtual ICollection<Store> Store { get; set; }

    }
}
