using Data.Databases;
using Data.Repository;
using Entity.Stores.Products;
using Repository.Stores.Product.IRepositorys;

namespace Repository.Stores.Product.Repositorys
{
    public class StoreProductCertificateRepository : GenericRepository<StoreProductCertificate>, IStoreProductCertificateRepository
    {
        public StoreProductCertificateRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
