using API.Helpers;
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
    public class SellerStoreCovargeLocationController : ControllerBase
    {
        private readonly IStoreCovargeLocationService _coverageService;
        private readonly ILocationService _marketLocationService;
        private readonly SellerUserContextHelper _userHelper;
        private readonly ILogger<SellerStoreCovargeLocationController> _logger;

        public SellerStoreCovargeLocationController(IStoreCovargeLocationService coverageService, ILocationService marketLocationService, SellerUserContextHelper userHelper, ILogger<SellerStoreCovargeLocationController> logger)
        {
            _coverageService = coverageService;
            _marketLocationService = marketLocationService;
            _userHelper = userHelper;
            _logger = logger;
        }

        //[HttpPost("add-coverage")]
        //public async Task<IActionResult> AddCoverage([FromBody] StoreMarketCoverageCreateDto dto)
        //{
        //    return null;
        //}

        //[HttpGet("get-my-coverage/{type}")]
        //public async Task<IActionResult> GetMyCoverage([FromRoute]  [FromBody] StoreMarketCoverageList dto)
        //{
        //    return null;

        //}

        //[HttpPost("delete-coverage")]
        //public async Task<IActionResult> DeleteCompositeCoverage([FromBody] StoreMarketCoverageCompositeDeleteDto dto)
        //{
        //    return null;
        //}
    }
}