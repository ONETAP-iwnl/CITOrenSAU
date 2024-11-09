using AuthService.Context;
using AuthService.DTO_Class;
using AuthService.Interface;
using AuthService.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Login) || string.IsNullOrWhiteSpace(model.Password))
            {
                return BadRequest("Invalid client request");
            }

            var userResponse = await _authService.AuthenticateAsync(model);

            if (userResponse == null)
            {
                return Unauthorized();
            }

            return Ok(userResponse);
        }
    }
}
