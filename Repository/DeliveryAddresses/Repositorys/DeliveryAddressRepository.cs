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

        public async Task<IEnumerable<DeliveryAddress>> GetAddressesByBuyerIdAsync(int buyerUserId)
        {
            return await _dbSet
                .Where(x => x.BuyerUserId == buyerUserId)
                .ToListAsync();
        }

        public async Task<DeliveryAddress> GetDefaultAddressAsync(int buyerUserId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(x => x.BuyerUserId == buyerUserId && x.IsDefault);
        }
    }
}
