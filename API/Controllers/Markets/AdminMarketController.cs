using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Markets
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "admin")]
    [Authorize]
    public class AdminMarketController : ControllerBase
    {
       
      
    }
}
