using Data.Databases;
using Data.Repository;
using Entity.Carriers;
using Repository.Carriers.IRepositorys;

namespace Repository.Carriers.Repositorys
{
    public class CarrierRepository : GenericRepository<Carrier>, ICarrierRepository
    {
        public CarrierRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
