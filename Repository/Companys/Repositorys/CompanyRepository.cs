using Data.Databases;
using Data.Repository;
using Entity.Companies;
using Microsoft.EntityFrameworkCore;
using Repository.Companys.IRepositorys;

namespace Repository.Companys.Repositorys
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Company> GetCompanyBySellerIdAsync(int sellerId)
        {
            return await _context.Companies
                .FirstOrDefaultAsync(c => c.SellerUserId == sellerId);
        }
    }
}
