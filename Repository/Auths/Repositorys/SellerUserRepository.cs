using Data.Databases;
using Data.Repository;
using Entity.Auths;
using Repository.Auths.IRepositorys;

namespace Repository.Auths.Repositorys
{
    public class SellerUserRepository : GenericRepository<SellerUser>, ISellerUserRepository
    {
        public SellerUserRepository(ApplicationDbContext context) : base(context) { }


    }
}
