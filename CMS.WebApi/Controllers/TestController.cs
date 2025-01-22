using CMS.WebApi.Controllers.Common;
using Microsoft.AspNetCore.Mvc;

namespace CMS.WebApi.Controllers
{
    public class TestController : BaseApiController
    {
        public TestController()
        {
        }

        [HttpGet("test")]
        public async Task<IActionResult> Get()
        {
            return Ok("Okk");
        }
    }
}
