using Data.Databases;
using Data.Repository;
using Entity.Auths;
using Microsoft.EntityFrameworkCore;
using Repository.Auths.IRepositorys;

namespace Repository.Auths.Repositorys
{
    public class AdminUserRepository : GenericRepository<AdminUser>, IAdminUserRepository
    {
        public AdminUserRepository(ApplicationDbContext context) : base(context) { }

        public async Task<AdminUser> GetAdminByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<AdminUser> GetSuperAdminAsync()
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.IsSuperAdmin);
        }

        public async Task<bool> IsSuperAdminExistsAsync()
        {
            return await _dbSet.AnyAsync(a => a.IsSuperAdmin);
        }

        public async Task<AdminUser> GetByAwsIamArnAsync(string arn)
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.AwsIamUserArn == arn);
        }
    }
}
