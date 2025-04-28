using Data.Dtos.Companies;
using Entity.Auths;

namespace Services.Companys.IServices
{
    public interface ICompanyService
    {
        // Buyer / Seller / Admin ortak kullanır
        Task<string> RegisterCompanyAsync(CompanyCreateDto companyCreateDto, int userId, UserType userType);
        Task<CompanyDto> GetCompanyByIdAsync(int companyId);

        // Seller 
        Task<CompanyDto> GetCompanyBySellerUserIdAsync(int sellerUserId);

        // Buyer 
        Task<CompanyDto> GetCompanyByBuyerUserIdAsync(int buyerUserId);

        // Admin işlemleri
        Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync();
        Task<bool> UpdateCompanyAsync(CompanyUpdateDto companyUpdateDto);
        Task<bool> VerifyCompanyAsync(int companyId, bool isVerified);
        Task<bool> ToggleCompanyStatusAsync(int companyId);
    }
}
