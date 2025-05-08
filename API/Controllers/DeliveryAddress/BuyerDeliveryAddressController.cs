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
        private readonly ILogger<BuyerDeliveryAddressController> _logger;

        public BuyerDeliveryAddressController(
            IDeliveryAddressService addressService,
            BuyerUserContextHelper buyerUserContextHelper,
            IMapper mapper,
            ILogger<BuyerDeliveryAddressController> logger)
        {
            _addressService = addressService;
            _buyerUserContextHelper = buyerUserContextHelper;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyAddresses()
        {
            try
            {
                int buyerId = _buyerUserContextHelper.GetBuyerId(User);
                _logger.LogInformation("Buyer {BuyerId} adres listesini görüntülüyor", buyerId);

                var addresses = await _addressService.GetAddressesByBuyerAsync(buyerId);
                return Ok(addresses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Adresler alınırken hata oluştu");
                return StatusCode(500, "Adresler alınırken bir hata oluştu.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                int buyerId = _buyerUserContextHelper.GetBuyerId(User);
                var address = await _addressService.GetAddressByIdAsync(id);

                if (address == null || address.BuyerUserId != buyerId)
                    return Unauthorized("Bu adrese erişim izniniz yok.");

                return Ok(address);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Adres detayları alınırken hata oluştu. Id: {AddressId}", id);
                return StatusCode(500, "Adres detayları alınırken bir hata oluştu.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAddress([FromBody] DeliveryAddressCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                int buyerId = _buyerUserContextHelper.GetBuyerId(User);
                var result = await _addressService.AddAddressAsync(dto, buyerId);

                if (!result)
                    return BadRequest("Adres eklenemedi. Lütfen bilgileri kontrol edin.");

                _logger.LogInformation("Buyer {BuyerId} için yeni adres eklendi", buyerId);
                return Ok("Adres başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Adres ekleme sırasında hata oluştu");
                return StatusCode(500, "Adres ekleme sırasında bir hata oluştu.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAddress([FromBody] DeliveryAddressUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                int buyerId = _buyerUserContextHelper.GetBuyerId(User);
                var result = await _addressService.UpdateAddressAsync(dto, buyerId);

                if (!result)
                    return BadRequest("Adres güncellenemedi. Adres bulunamadı veya size ait değil.");

                _logger.LogInformation("Buyer {BuyerId} adresi güncelledi. AddressId: {AddressId}", buyerId, dto.Id);
                return Ok("Adres başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Adres güncellenirken hata oluştu. AddressId: {AddressId}", dto.Id);
                return StatusCode(500, "Adres güncellenirken bir hata oluştu.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            try
            {
                int buyerId = _buyerUserContextHelper.GetBuyerId(User);
                var result = await _addressService.DeleteAddressAsync(id, buyerId);

                if (!result)
                    return BadRequest("Adres silinemedi. Adres bulunamadı veya size ait değil.");

                _logger.LogInformation("Buyer {BuyerId} adresi sildi. AddressId: {AddressId}", buyerId, id);
                return Ok("Adres başarıyla silindi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Adres silinirken hata oluştu. AddressId: {AddressId}", id);
                return StatusCode(500, "Adres silinirken bir hata oluştu.");
            }
        }

        [HttpPost("{id}/set-default")]
        public async Task<IActionResult> SetAsDefault(int id)
        {
            try
            {
                int buyerId = _buyerUserContextHelper.GetBuyerId(User);
                var result = await _addressService.SetAsDefaultAsync(buyerId, id);

                if (!result)
                    return BadRequest("Varsayılan adres güncellenemedi.");

                _logger.LogInformation("Buyer {BuyerId} varsayılan adresini değiştirdi. AddressId: {AddressId}", buyerId, id);
                return Ok("Varsayılan adres başarıyla ayarlandı.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Varsayılan adres atanırken hata oluştu. AddressId: {AddressId}", id);
                return StatusCode(500, "Varsayılan adres ayarlanırken bir hata oluştu.");
            }
        }
    }
}
