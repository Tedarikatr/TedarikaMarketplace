using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Stores.Markets.IServices;

namespace API.Controllers.Stores.Market
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "seller")]
    [Authorize]
    public class SellerMarketCoverageController : ControllerBase
    {
        private readonly IStoreMarketCoverageService _coverageService;
        private readonly SellerUserContextHelper _userHelper;
        private readonly ILogger<SellerMarketCoverageController> _logger;

        public SellerMarketCoverageController(IStoreMarketCoverageService coverageService, SellerUserContextHelper userHelper, ILogger<SellerMarketCoverageController> logger)
        {
            _coverageService = coverageService;
            _userHelper = userHelper;
            _logger = logger;
        }
  
    }
}