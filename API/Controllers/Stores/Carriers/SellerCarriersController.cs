using API.Helpers;
using Data.Dtos.Stores.Carriers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Stores.Carriers.IServices;

namespace API.Controllers.Stores.Carriers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "seller")]
    [Authorize]
    public class SellerCarriersController : ControllerBase
    {
        private readonly IStoreCarrierService _storeCarrierService;
        private readonly SellerUserContextHelper _sellerHelper;
        private readonly ILogger<SellerCarriersController> _logger;

        public SellerCarriersController(
            IStoreCarrierService storeCarrierService,
            SellerUserContextHelper sellerHelper,
            ILogger<SellerCarriersController> logger)
        {
            _storeCarrierService = storeCarrierService;
            _sellerHelper = sellerHelper;
            _logger = logger;
        }

        [HttpGet("my-store")]
        public async Task<IActionResult> GetMyStoreCarriers()
        {
            try
            {
                var storeId = await _sellerHelper.GetStoreId(User);
                var result = await _storeCarrierService.GetStoreCarriersAsync(storeId);

                _logger.LogInformation("StoreId {StoreId} için kargo firmaları başarıyla listelendi.", storeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kargo firmaları listelenirken hata oluştu.");
                return StatusCode(500, "Kargo firmaları listelenemedi.");
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCarrier([FromBody] StoreCarrierCreateDto dto)
        {
            try
            {
                var storeId = await _sellerHelper.GetStoreId(User);
                dto.StoreId = storeId;

                var success = await _storeCarrierService.AddCarrierToStoreAsync(dto);

                if (!success)
                {
                    _logger.LogWarning("StoreId {StoreId} için CarrierId {CarrierId} zaten atanmış.", storeId, dto.CarrierId);
                    return BadRequest("Bu kargo firması mağazaya zaten eklenmiş.");
                }

                _logger.LogInformation("StoreId {StoreId} için CarrierId {CarrierId} başarıyla eklendi.", storeId, dto.CarrierId);
                return Ok("Kargo firması mağazaya eklendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kargo firması mağazaya eklenirken hata oluştu.");
                return StatusCode(500, "Kargo firması eklenemedi.");
            }
        }

        [HttpDelete("remove/{storeCarrierId}")]
        public async Task<IActionResult> RemoveCarrier(int storeCarrierId)
        {
            try
            {
                var success = await _storeCarrierService.RemoveCarrierFromStoreAsync(storeCarrierId);

                if (!success)
                {
                    _logger.LogWarning("StoreCarrier silinemedi. Id: {StoreCarrierId}", storeCarrierId);
                    return NotFound("Kargo firması bağlantısı bulunamadı.");
                }

                _logger.LogInformation("StoreCarrier bağlantısı başarıyla silindi. Id: {StoreCarrierId}", storeCarrierId);
                return Ok("Kargo firması mağazadan silindi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kargo firması silinirken hata oluştu. Id: {StoreCarrierId}", storeCarrierId);
                return StatusCode(500, "Silme işlemi sırasında bir hata oluştu.");
            }
        }

        [HttpPut("enable/{storeCarrierId}")]
        public async Task<IActionResult> EnableCarrier(int storeCarrierId)
        {
            try
            {
                var success = await _storeCarrierService.EnableCarrierAsync(storeCarrierId);
                if (!success)
                {
                    _logger.LogWarning("Aktif edilecek StoreCarrier bulunamadı. Id: {StoreCarrierId}", storeCarrierId);
                    return NotFound("Kargo firması bağlantısı bulunamadı.");
                }

                _logger.LogInformation("StoreCarrier aktif hale getirildi. Id: {StoreCarrierId}", storeCarrierId);
                return Ok("Kargo firması aktifleştirildi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kargo firması aktif edilirken hata oluştu. Id: {StoreCarrierId}", storeCarrierId);
                return StatusCode(500, "Aktifleştirme işlemi sırasında bir hata oluştu.");
            }
        }

        [HttpPut("disable/{storeCarrierId}")]
        public async Task<IActionResult> DisableCarrier(int storeCarrierId)
        {
            try
            {
                var success = await _storeCarrierService.DisableCarrierAsync(storeCarrierId);
                if (!success)
                {
                    _logger.LogWarning("Pasif edilecek StoreCarrier bulunamadı. Id: {StoreCarrierId}", storeCarrierId);
                    return NotFound("Kargo firması bağlantısı bulunamadı.");
                }

                _logger.LogInformation("StoreCarrier pasif hale getirildi. Id: {StoreCarrierId}", storeCarrierId);
                return Ok("Kargo firması pasifleştirildi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kargo firması pasifleştirilirken hata oluştu. Id: {StoreCarrierId}", storeCarrierId);
                return StatusCode(500, "Pasifleştirme işlemi sırasında bir hata oluştu.");
            }
        }
    }
}
