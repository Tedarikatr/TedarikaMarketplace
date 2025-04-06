using Data.Dtos.DeliveryAddresses;
using Microsoft.AspNetCore.Mvc;
using Services.DeliveryAddress.IService;
using System.Security.Claims;

namespace API.Controllers.DeliveryAddress
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyerDeliveryAddressController : ControllerBase
    {
        private readonly IDeliveryAddressService _deliveryAddressService;
        private readonly IAddressValidationService _addressValidationService;

        public BuyerDeliveryAddressController(IDeliveryAddressService deliveryAddressService, IAddressValidationService addressValidationService)
        {
            _deliveryAddressService = deliveryAddressService;
            _addressValidationService = addressValidationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            int buyerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var addresses = await _deliveryAddressService.GetAddressesByBuyerIdAsync(buyerId);
            return Ok(addresses);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DeliveryAddressCreateDto dto)
        {
            dto.BuyerUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _deliveryAddressService.AddAddressAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DeliveryAddressUpdateDto dto)
        {
            var success = await _deliveryAddressService.UpdateAddressAsync(dto);
            return success ? Ok("Adres güncellendi.") : NotFound("Adres bulunamadı.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _deliveryAddressService.DeleteAddressAsync(id);
            return success ? Ok("Adres silindi.") : NotFound("Adres bulunamadı.");
        }

        [HttpPost("set-default/{addressId}")]
        public async Task<IActionResult> SetDefault(int addressId)
        {
            int buyerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var success = await _deliveryAddressService.SetDefaultAsync(buyerId, addressId);
            return success ? Ok("Varsayılan adres belirlendi.") : BadRequest("İşlem başarısız.");
        }

        [HttpPost("validate")]
        public async Task<IActionResult> ValidateAddress([FromBody] DeliveryAddressValidateDto dto)
        {
            var result = await _addressValidationService.ValidateAsync(dto);
            return Ok(result);
        }
    }
}
