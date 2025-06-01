using Data.Dtos.Forms;

namespace Services.Forms.IServices
{
    public interface ISellerApplicationService
    {
        // BAŞVURUSAL İŞLEMLER (User-facing)
        Task<int> CreateAsync(SellerApplicationCreateDto dto); 
        Task<SellerApplicationDetailDto?> GetByGuidAsync(Guid guidId);

        // ADMİNSEL İŞLEMLER (Admin Panel)
        Task<List<SellerApplicationListDto>> GetAllAsync();
        Task<SellerApplicationDetailDto?> GetDetailByIdAsync(int id); 
        Task<bool> UpdateApprovalAsync(SellerApplicationUpdateApprovalDto dto); 
        Task<bool> DeleteAsync(int id); 
    }
}
