using Data.Databases;
using Data.Repository;
using Entity.DeliveryAddresses;
using Microsoft.EntityFrameworkCore;
using Repository.DeliveryAddresses.IRepositorys;

namespace Repository.DeliveryAddresses.Repositorys
{
    public class DeliveryAddressRepository : GenericRepository<DeliveryAddress>, IDeliveryAddressRepository
    {
        public DeliveryAddressRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<DeliveryAddress> GetDefaultAddressByBuyerIdAsync(int buyerId)
        {
            return await _dbSet
                .Include(x => x.Country)
                .Include(x => x.State)
                .Include(x => x.Province)
                .Include(x => x.District)
                .Include(x => x.Neighborhood)
                .FirstOrDefaultAsync(x => x.BuyerUserId == buyerId && x.IsDefault);
        }
    }
}
