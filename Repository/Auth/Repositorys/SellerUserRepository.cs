using Data.Database;
using Data.Repository;
using Entity.Auth;
using Microsoft.EntityFrameworkCore;
using Repository.Auth.IRepositorys;

namespace Repository.Auth.Repositorys
{
    public class SellerUserRepository : GenericRepository<SellerUser>, ISellerUserRepository
    {
        public SellerUserRepository(ApplicationDbContext context) : base(context) { }

      
    }
}
