using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Context;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public readonly UserContext _userContext;

        public UserController(UserContext userContext)
        {
            _userContext = userContext;
        }


        [HttpGet("allUser")]
        public async Task<IActionResult> GetAllUserAsync() //получение всех юзер
        {
            var user = await _userContext.Users.ToListAsync();

            if (user == null || user.Count == 0)
            {
                return BadRequest("Invalid user data.");
            }
            try
            {
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
