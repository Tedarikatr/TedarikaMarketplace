using Data.Databases;
using Data.Repository;
using Entity.Auths;
using Microsoft.EntityFrameworkCore;
using Repository.Auths.IRepositorys;

namespace Repository.Auths.Repositorys
{
    public class BuyerUserRepository : GenericRepository<BuyerUser>, IBuyerUserRepository
    {
        public BuyerUserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<BuyerUser> GetByAwsIamArnAsync(string arn)
        {
            return await _dbSet.FirstOrDefaultAsync(b => b.AwsIamUserArn == arn);
        }
    }
}
