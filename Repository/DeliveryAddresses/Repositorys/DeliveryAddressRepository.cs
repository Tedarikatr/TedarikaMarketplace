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

        public async Task<List<DeliveryAddress>> GetAllWithLocationByBuyerIdAsync(int buyerUserId)
        {
            return await _context.DeliveryAddresses
                .Where(a => a.BuyerUserId == buyerUserId)
                .Include(a => a.Region)
                .Include(a => a.Country)
                .Include(a => a.State)
                .Include(a => a.Province)
                .Include(a => a.District)
                .Include(a => a.Neighborhood)
                .ToListAsync();
        }

        public async Task<DeliveryAddress> GetWithLocationByIdAsync(int id)
        {
            return await _context.DeliveryAddresses
                .Include(a => a.Region)
                .Include(a => a.Country)
                .Include(a => a.State)
                .Include(a => a.Province)
                .Include(a => a.District)
                .Include(a => a.Neighborhood)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<DeliveryAddress> GetDefaultWithLocationByBuyerIdAsync(int buyerUserId)
        {
            return await _context.DeliveryAddresses
                .Include(a => a.Region)
                .Include(a => a.Country)
                .Include(a => a.State)
                .Include(a => a.Province)
                .Include(a => a.District)
                .Include(a => a.Neighborhood)
                .FirstOrDefaultAsync(a => a.BuyerUserId == buyerUserId && a.IsDefault);
        }

    }
}
