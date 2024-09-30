using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Regiconnect.Api.Models;
using Regiconnect.Api.Services;

namespace Regiconnect.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SProcLoginController : ControllerBase
    {
        private readonly SProcLoginService _loginService;

        public SProcLoginController(SProcLoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
        {
            try
            {
                var result = await _loginService.LoginAsync(request);
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid username or password.");
            }
        }
    }
}
