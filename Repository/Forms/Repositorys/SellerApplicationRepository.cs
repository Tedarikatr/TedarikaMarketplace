using Data.Databases;
using Data.Repository;
using Entity.Forms;
using Microsoft.EntityFrameworkCore;
using Repository.Forms.IRepositorys;

namespace Repository.Forms.Repositorys
{
    public class SellerApplicationRepository : GenericRepository<SellerApplication>, ISellerApplicationRepository
    {
        private readonly ApplicationDbContext _context;

        public SellerApplicationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<SellerApplication?> GetByGuidAsync(Guid guidId)
        {
            return await _context.SellerApplications
                .FirstOrDefaultAsync(x => x.GuidId == guidId);
        }
    }
}
