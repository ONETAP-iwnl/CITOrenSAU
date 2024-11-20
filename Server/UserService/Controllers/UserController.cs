using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UserService.Context;
using UserService.Interface;
using UserService.Model;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserFactory userFactory)
        {
            var userRepository = userFactory.CreateUserRepository();
            _userService = userFactory.CreateUserService(userRepository);
        }

        [HttpGet("allUser")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Users newUser)
        {
            Console.WriteLine($"Получен запрос на регистрацию: {JsonConvert.SerializeObject(newUser)}");
            try
            {
                if (!ModelState.IsValid)
                {
                    Console.WriteLine("Ошибка валидации модели.");
                    return BadRequest(ModelState);
                }

                await _userService.AddUserAsync(newUser);
                Console.WriteLine("Пользователь успешно зарегистрирован.");
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return StatusCode(500, "Ошибка сервера");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] Users user)
        {
            if (user == null || id != user.ID_User)
            {
                return BadRequest("Invalid user data.");
            }

            await _userService.UpdateUserAsync(user);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
