using Data.Databases;
using Data.Repository;
using Entity.Auths;
using Microsoft.EntityFrameworkCore;
using Repository.Auths.IRepositorys;

namespace Repository.Auths.Repositorys
{
    public class SellerUserRepository : GenericRepository<SellerUser>, ISellerUserRepository
    {
        public SellerUserRepository(ApplicationDbContext context) : base(context) { }

        public async Task<SellerUser> GetByAwsIamArnAsync(string arn)
        {
            return await _dbSet.FirstOrDefaultAsync(s => s.AwsIamUserArn == arn);
        }
    }
}
