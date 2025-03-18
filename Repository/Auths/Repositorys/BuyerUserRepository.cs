using Data.Databases;
using Data.Repository;
using Entity.Auths;
using Repository.Auths.IRepositorys;

namespace Repository.Auths.Repositorys
{
    public class BuyerUserRepository : GenericRepository<BuyerUser>, IBuyerUserRepository
    {
        public BuyerUserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
