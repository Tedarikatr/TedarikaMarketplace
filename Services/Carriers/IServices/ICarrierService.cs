using Data.Dtos.Carriers;

namespace Services.Carriers.IServices
{
    public interface ICarrierService
    {
        Task<List<CarrierDto>> GetAllCarriersAsync();
        Task<CarrierDto> GetCarrierByIdAsync(int id);
        Task<int> CreateCarrierAsync(CarrierCreateDto dto);
        Task<bool> DeleteCarrierAsync(int id);
    }
}

