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

        public async Task<SellerUser> GetUserByEmailAsync(string email)
        {
            return await _context.SellerUsers.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.SellerUsers.AnyAsync(u => u.Email == email);
        }
    }
}
