using Data.Dtos.Forms;

namespace Services.Forms.IServices
{
    public interface IBuyerApplicationService
    {
        Task<bool> CreateBuyerApplicationAsync(BuyerApplicationCreateDto dto, string ipAddress);
        Task<IEnumerable<BuyerApplicationDto>> GetApplicationsByBuyerAsync(Guid buyerUserId);

        Task<IEnumerable<BuyerApplicationDto>> GetAllApplicationsAsync();

        Task<BuyerApplicationDto> GetByIdAsync(int id);

        Task<bool> MarkAsFulfilledAsync(int id);

    }
}
