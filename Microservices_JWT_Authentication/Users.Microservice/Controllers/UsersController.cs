using Microsoft.AspNetCore.Mvc;

namespace Users.Microservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private static readonly string[] UserNames = new[] { "Alpha", "Beta", "Gamma", "Delta", "Epsilon" };

        [HttpGet(Name = "GetUsers")]
        public IEnumerable<Users> Get()
        {
            return Enumerable.Range(0, 5).Select(index => new Users
            {
                Id = index + 1,
                Name = UserNames[index],
                Email = $"{UserNames[index]}@gmail.com"
            })
            .ToArray();
        }
    }
}