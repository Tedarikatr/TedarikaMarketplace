using Data.Databases;
using Data.Repository;
using Entity.Companies;
using Repository.Companys.IRepositorys;

namespace Repository.Companys.Repositorys
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
