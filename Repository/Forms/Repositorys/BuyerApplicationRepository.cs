using Data.Databases;
using Data.Repository;
using Entity.Forms;
using Repository.Forms.IRepositorys;

namespace Repository.Forms.Repositorys
{
    public class BuyerApplicationRepository : GenericRepository<BuyerApplication>, IBuyerApplicationRepository
    {
        private readonly ApplicationDbContext _context;

        public BuyerApplicationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
