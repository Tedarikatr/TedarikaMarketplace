using Data.Repository;
using Entity.Companies;

namespace Repository.Companys.IRepositorys
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {
        Task<Company> GetCompanyBySellerIdAsync(int sellerId);
    }
}
