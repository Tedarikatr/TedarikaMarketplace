using Data.Repository;
using Entity.Auths;

namespace Repository.Auths.IRepositorys
{
    public interface IAdminUserRepository : IGenericRepository<AdminUser>
    {
        Task<AdminUser> GetAdminByEmailAsync(string email);
        Task<AdminUser> GetSuperAdminAsync();
        Task<bool> IsSuperAdminExistsAsync();
    }
}
