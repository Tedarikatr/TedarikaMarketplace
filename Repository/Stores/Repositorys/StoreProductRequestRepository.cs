using Data.Databases;
using Data.Repository;
using Entity.Stores.Products;
using Microsoft.EntityFrameworkCore;
using Repository.Stores.IRepositorys;

namespace Repository.Stores.Repositorys
{
    public class StoreProductRequestRepository : GenericRepository<StoreProductRequest>, IStoreProductRequestRepository
    {
        public StoreProductRequestRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<StoreProductRequest>> GetPendingRequestsAsync()
        {
            return await _dbSet.Where(x => !x.IsApproved).ToListAsync();
        }
    }
}
