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
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsService _authorService;

        public AuthorsController(IAuthorsService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet("allAuthors")]
        public async Task<IActionResult> GetAllAuthorsAsync()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            return Ok(authors);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<Authors>> GetAuthorByUserId(int userId)
        {
            var author = await _authorService.GetAuthorsByUserIdAsync(userId);
            return Ok(author);
        }
    }
}