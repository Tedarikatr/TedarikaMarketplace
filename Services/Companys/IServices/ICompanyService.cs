using Data.Dtos.Companys;

namespace Services.Companys.IServices
{
    public interface ICompanyService
    {
        Task<string> RegisterCompanyAsync(CompanyCreateDto companyCreateDto);
        Task<CompanyDto> GetCompanyByIdAsync(int companyId);
        Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync();
        Task<bool> UpdateCompanyAsync(CompanyUpdateDto companyUpdateDto);
        Task<bool> VerifyCompanyAsync(int companyId, bool isVerified);
        Task<bool> ChangeCompanyStatusAsync(int companyId, bool isActive);
    }
}
