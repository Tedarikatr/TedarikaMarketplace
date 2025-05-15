using Data.Databases;
using Data.Repository;
using Entity.Incoterms;
using Repository.Incoterms.Repositorys;

namespace Repository.Incoterms.IRepositorys
{
    public class IncotermRepository : GenericRepository<Incoterm>, IIncotermRepository
    {
        public IncotermRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
