using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Regiconnect.Api.Models;
using Regiconnect.Api.Services;

namespace Regiconnect.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SProcSignupController : ControllerBase
    {
        private readonly SProcSignupService _signupService;

        public SProcSignupController(SProcSignupService signupService)
        {
            _signupService = signupService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] SignupRequestModel request)
        {
            var result = await _signupService.RegisterAsync(request);
            return Ok(result);
        }
    }
}
