using Data.Dtos.DeliveryAddresses;
using Microsoft.Extensions.Logging;
using Services.DeliveryAddress.IService;
using System.Net.Http.Json;

namespace Services.DeliveryAddress.Services
{
    public class AddressValidationService : IAddressValidationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AddressValidationService> _logger;

        public AddressValidationService(HttpClient httpClient, ILogger<AddressValidationService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<AddressValidationResultDto> ValidateAsync(DeliveryAddressValidateDto dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://api.partnercenter.microsoft.com/validateaddress", dto);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<AddressValidationResultDto>();
                    return result!;
                }

                return new AddressValidationResultDto
                {
                    IsValid = false,
                    Message = $"Doğrulama API hatası: {response.StatusCode}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Adres doğrulama sırasında bir hata oluştu.");
                return new AddressValidationResultDto
                {
                    IsValid = false,
                    Message = "Adres doğrulama sırasında bir hata oluştu."
                };
            }
        }
    }
}
