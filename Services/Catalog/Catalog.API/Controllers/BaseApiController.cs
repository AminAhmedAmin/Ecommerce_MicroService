using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiversion}/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
    }
}

