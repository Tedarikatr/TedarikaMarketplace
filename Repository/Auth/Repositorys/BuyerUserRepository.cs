using Data.Database;
using Data.Repository;
using Entity.Auth;
using Repository.Auth.IRepositorys;

namespace Repository.Auth.Repositorys
{
    public class BuyerUserRepository : GenericRepository<BuyerUser>, IBuyerUserRepository
    {
        public BuyerUserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
