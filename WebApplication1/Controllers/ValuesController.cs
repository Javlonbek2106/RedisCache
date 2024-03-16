using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IClass _c;

        public ValuesController(IClass c)
        {
            _c = c;
        }
        [HttpGet]
        public async Task<User> Get(int id)
        {
            return await _c.GetUser(id);
        }
    }
}
