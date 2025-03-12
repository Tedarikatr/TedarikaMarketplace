using Data.Repository;
using Entity.Auth;

namespace Repository.Auth.IRepositorys
{
    public interface ISellerUserRepository : IGenericRepository<SellerUser>
    {
        Task<SellerUser> GetUserByEmailAsync(string email);
        Task<bool> EmailExistsAsync(string email);
    }
}
