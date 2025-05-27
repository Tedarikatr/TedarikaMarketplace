using Data.Repository;
using Entity.Auths;

namespace Repository.Auths.IRepositorys
{
    public interface IBuyerUserRepository : IGenericRepository<BuyerUser>
    {
        Task<BuyerUser> GetByAwsIamArnAsync(string arn);
    }
}
