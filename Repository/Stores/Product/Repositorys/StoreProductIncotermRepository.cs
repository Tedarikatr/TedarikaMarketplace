using Data.Databases;
using Data.Repository;
using Entity.Stores.Products;
using Repository.Stores.Product.IRepositorys;

namespace Repository.Stores.Product.Repositorys
{
    public class StoreProductIncotermRepository : GenericRepository<StoreProductIncoterm>, IStoreProductIncotermRepository
    {
        public StoreProductIncotermRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
