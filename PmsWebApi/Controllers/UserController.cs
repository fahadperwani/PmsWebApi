using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PmsWebApi.Models;
using PmsWebApi.Services;

namespace PmsWebApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        public UserController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> register(Registration User)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterUser(User);

            if (result.Succeeded) return Ok("Done");

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Login User)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.LoginUser(User);

                if(result == null) return BadRequest("Invalid Username or password");

                return Ok(result);
            }
            return BadRequest("Invalid Username or password");
        }
    }
}
