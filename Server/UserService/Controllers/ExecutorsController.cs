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
    public class ExecutorsController : ControllerBase
    {
        private readonly IExecutorsService _executorsService;

        public ExecutorsController(IExecutorsService executorsService)
        {
            _executorsService = executorsService;
        }

        [HttpGet("allExecutors")]
        public async Task<IActionResult> GetAllExecutorsAsync()
        {
            var executors = await _executorsService.GetAllExecutorsAsync();
            return Ok(executors);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<Authors>> GetAuthorByUserId(int userId)
        {
            var author = await _executorsService.GetExecutorsByUserIdAsync(userId);
            return Ok(author);
        }
    }
}