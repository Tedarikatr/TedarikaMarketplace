using Data.Repository;
using Entity.Auths;

namespace Repository.Auths.IRepositorys
{
    public interface ISellerUserRepository : IGenericRepository<SellerUser>
    {
        Task<SellerUser> GetByAwsIamArnAsync(string arn);
    }
}
