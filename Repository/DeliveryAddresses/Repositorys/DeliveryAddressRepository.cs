using Data.Databases;
using Data.Repository;
using Entity.DeliveryAddresses;
using Repository.DeliveryAddresses.IRepositorys;

namespace Repository.DeliveryAddresses.Repositorys
{
    public class DeliveryAddressRepository : GenericRepository<DeliveryAddress>, IDeliveryAddressRepository
    {
        public DeliveryAddressRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
