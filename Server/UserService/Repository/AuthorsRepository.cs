using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using UserService.Context;
using UserService.Interface;
using UserService.Model;

namespace UserService.Repository
{
    public class AuthorsRepository : IAuthorsRepository
    {
        private readonly AuthorsContext _context;

        public AuthorsRepository(AuthorsContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Authors>> GetAllAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Authors> GetAuthorsByUserIdAsync(int userId)
        {
            return await _context.Authors.FirstOrDefaultAsync(x => x.ID_User == userId);
        }
    }
}