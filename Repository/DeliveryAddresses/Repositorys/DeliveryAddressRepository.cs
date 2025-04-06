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

        public async Task<DeliveryAddress> GetByIdWithBuyerAsync(int id, int buyerUserId)
        {
            return await _context.DeliveryAddresses
                .FirstOrDefaultAsync(x => x.Id == id && x.BuyerUserId == buyerUserId);
        }
    }
}
