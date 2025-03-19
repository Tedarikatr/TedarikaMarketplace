using Data.Dtos.Companies;
using Entity.Auths;

namespace Services.Companys.IServices
{
    public interface ICompanyService
    {
        Task<string> RegisterCompanyAsync(CompanyCreateDto companyCreateDto, int userId, UserType userType);
        Task<CompanyDto> GetCompanyByIdAsync(int companyId);
        Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync();
        Task<bool> UpdateCompanyAsync(CompanyUpdateDto companyUpdateDto);
        Task<bool> VerifyCompanyAsync(int companyId);
        Task<bool> ToggleCompanyStatusAsync(int companyId);
    }
}
