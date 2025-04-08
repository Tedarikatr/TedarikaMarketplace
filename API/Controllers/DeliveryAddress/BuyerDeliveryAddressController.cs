using API.Helpers;
using AutoMapper;
using Data.Dtos.DeliveryAddresses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DeliveryAddress.IService;

namespace API.Controllers.DeliveryAddress
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "buyer")]
    [Authorize]
    public class BuyerDeliveryAddressController : ControllerBase
    {
        private readonly IDeliveryAddressService _addressService;
        private readonly IMapper _mapper;

        public BuyerDeliveryAddressController(IDeliveryAddressService addressService, IMapper mapper)
        {
            _addressService = addressService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyAddresses()
        {
            int buyerId = BuyerUserContextHelper.GetBuyerId(User);
            var result = await _addressService.GetAddressesByBuyerAsync(buyerId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAddress([FromBody] DeliveryAddressDto dto)
        {
            int buyerId = BuyerUserContextHelper.GetBuyerId(User);

            var address = _mapper.Map<Entity.DeliveryAddresses.DeliveryAddress>(dto);
            address.BuyerUserId = buyerId;

            await _addressService.AddAddressAsync(address);
            return Ok("Adres başarıyla eklendi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(int id, [FromBody] DeliveryAddressDto dto)
        {
            var address = await _addressService.GetAddressByIdAsync(id);
            if (address == null) return NotFound("Adres bulunamadı.");

            int buyerId = BuyerUserContextHelper.GetBuyerId(User);
            if (address.BuyerUserId != buyerId)
                return Forbid("Bu adres size ait değil.");

            _mapper.Map(dto, address);
            await _addressService.UpdateAddressAsync(address);
            return Ok("Adres güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var address = await _addressService.GetAddressByIdAsync(id);
            if (address == null) return NotFound("Adres bulunamadı.");

            int buyerId = BuyerUserContextHelper.GetBuyerId(User);
            if (address.BuyerUserId != buyerId)
                return Forbid("Bu adres size ait değil.");

            await _addressService.DeleteAddressAsync(id);
            return Ok("Adres silindi.");
        }

        [HttpPost("{id}/set-default")]
        public async Task<IActionResult> SetAsDefault(int id)
        {
            int buyerId = BuyerUserContextHelper.GetBuyerId(User);
            await _addressService.SetAsDefaultAsync(buyerId, id);
            return Ok("Varsayılan adres ayarlandı.");
        }
    }
}
