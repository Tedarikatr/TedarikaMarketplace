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
    [Authorize]
    [ApiExplorerSettings(GroupName = "buyer")]
    public class BuyerDeliveryAddressController : ControllerBase
    {
        private readonly IDeliveryAddressService _addressService;
        private readonly BuyerUserContextHelper _buyerUserContextHelper;
        private readonly IMapper _mapper;

        public BuyerDeliveryAddressController(IDeliveryAddressService addressService, BuyerUserContextHelper buyerUserContextHelper, IMapper mapper)
        {
            _addressService = addressService;
            _buyerUserContextHelper = buyerUserContextHelper;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyAddresses()
        {
            int buyerId = _buyerUserContextHelper.GetBuyerId(User);

            var addresses = await _addressService.GetAddressesByBuyerAsync(buyerId);
            return Ok(addresses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var address = await _addressService.GetAddressByIdAsync(id);
            if (address == null)
                return NotFound("Adres bulunamadı.");

            return Ok(address);
        }

        [HttpPost]
        public async Task<IActionResult> AddAddress([FromBody] DeliveryAddressCreateDto dto)
        {
            int buyerId = _buyerUserContextHelper.GetBuyerId(User);

            var result = await _addressService.AddAddressAsync(dto, buyerId);
            if (!result)
                return BadRequest("Adres eklenemedi. Lütfen bilgileri kontrol edin.");

            return Ok("Adres başarıyla eklendi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAddress([FromBody] DeliveryAddressUpdateDto dto)
        {
            int buyerId = _buyerUserContextHelper.GetBuyerId(User);

            var result = await _addressService.UpdateAddressAsync(dto, buyerId);
            if (!result)
                return BadRequest("Adres güncellenemedi. Adres bulunamadı veya size ait değil.");

            return Ok("Adres başarıyla güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            int buyerId = _buyerUserContextHelper.GetBuyerId(User);

            var result = await _addressService.DeleteAddressAsync(id, buyerId);
            if (!result)
                return BadRequest("Adres silinemedi. Adres bulunamadı veya size ait değil.");

            return Ok("Adres başarıyla silindi.");
        }

        [HttpPost("{id}/set-default")]
        public async Task<IActionResult> SetAsDefault(int id)
        {
            int buyerId = _buyerUserContextHelper.GetBuyerId(User);

            var result = await _addressService.SetAsDefaultAsync(buyerId, id);
            if (!result)
                return BadRequest("Varsayılan adres güncellenemedi.");

            return Ok("Varsayılan adres başarıyla ayarlandı.");
        }
    }
}
