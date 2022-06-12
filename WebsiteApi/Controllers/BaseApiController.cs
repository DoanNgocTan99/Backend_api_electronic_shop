using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebsiteApi.Helpers;

namespace WebsiteApi.Controllers
{
    /// <summary>
    ///  Class common 
    /// </summary>
    [EnableCors("AllowOrigin")]
    [Route("[controller]")]
    [ApiController]
    public class BaseApiController : Controller
    {
        
    }
}
