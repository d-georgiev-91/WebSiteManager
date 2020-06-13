using Microsoft.AspNetCore.Mvc;

namespace WebSiteManager.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebSitesController : ControllerBase
    {
        public WebSitesController()
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok("test");
        }
    }
}
