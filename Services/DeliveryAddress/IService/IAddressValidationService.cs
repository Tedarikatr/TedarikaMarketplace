using Data.Dtos.DeliveryAddresses;

namespace Services.DeliveryAddress.IService
{
    public interface IAddressValidationService
    {
        Task<AddressValidationResultDto> ValidateAsync(DeliveryAddressValidateDto dto);
    }
}
