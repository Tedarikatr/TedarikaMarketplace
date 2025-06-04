using API.Helpers;
using Data.Dtos.Stores.Locations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Locations.IServices;
using Services.Stores.Locations.IServices;

namespace API.Controllers.Stores.Locations
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "seller")]
    [Authorize]
    public class SellerStoreCoverageController : ControllerBase
    {
        private readonly IStoreCoverageService _coverageService;
        private readonly ILocationService _locationService;
        private readonly SellerUserContextHelper _userHelper;
        private readonly ILogger<SellerStoreCoverageController> _logger;

        public SellerStoreCoverageController(IStoreCoverageService coverageService, ILocationService locationService, SellerUserContextHelper userHelper, ILogger<SellerStoreCoverageController> logger)
        {
            _coverageService = coverageService;
            _locationService = locationService;
            _userHelper = userHelper;
            _logger = logger;
        }

        [HttpGet("my-coverage")]
        public async Task<IActionResult> GetMyCoverage()
        {
            var storeId = await _userHelper.GetStoreId(User);
            var result = await _coverageService.GetCoverageByStoreIdAsync(storeId);
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCoverage([FromBody] StoreCoverageCreateDto dto)
        {
            try
            {
                var storeId = await _userHelper.GetStoreId(User);
                var id = await _coverageService.AddCoverageAsync(dto, storeId);
                return Ok(id);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteCoverage([FromBody] StoreCoverageDeleteDto dto)
        {
            var storeId = await _userHelper.GetStoreId(User);
            var success = await _coverageService.DeleteCoverageAsync(dto, storeId);
            if (!success) return NotFound();
            return Ok();
        }
    } }