using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Stores.Product.IServices;

namespace API.Controllers.Stores.Products
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "admin")]
    [Authorize]
    public class AdminStoreProductRequestController : ControllerBase
    {
        private readonly IStoreProductRequestService _requestService;
        private readonly ILogger<AdminStoreProductRequestController> _logger;

        public AdminStoreProductRequestController(IStoreProductRequestService requestService, ILogger<AdminStoreProductRequestController> logger)
        {
            _requestService = requestService;
            _logger = logger;
        }

      
    }
}
