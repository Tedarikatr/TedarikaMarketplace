using Data.Databases;
using Data.Repository;
using Entity.Stores;

namespace Repository.Stores
{
    public class StoreRepository : GenericRepository<Store>, IStoreRepository
    {
        public StoreRepository(ApplicationDbContext context) : base(context) { }

        public Task<Store> GetStoreBySellerIdAsync(int sellerId)
        {
            throw new NotImplementedException();
        }
    }
}
