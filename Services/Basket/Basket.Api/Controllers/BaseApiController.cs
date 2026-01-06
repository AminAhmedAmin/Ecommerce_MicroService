using Asp.Versioning;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiversion}/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
    }
}
