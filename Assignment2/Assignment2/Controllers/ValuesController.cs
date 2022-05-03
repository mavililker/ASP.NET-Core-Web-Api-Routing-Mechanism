using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetValues()
        {
            return Ok("Values");
        }

        [HttpGet]
        public IActionResult GetValues2()
        {
            return Ok("Values2");
        }
    }
}
