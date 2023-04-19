using Microsoft.AspNetCore.Mvc;

namespace ProactAccouting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeGenerate : ControllerBase
    {
        [HttpPost]
        public IActionResult Generate()
        {

            return Ok();
        }
    }
}
