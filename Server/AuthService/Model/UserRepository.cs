using AuthService.Context;
using AuthService.Interface;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Model
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthContext _context;

        public UserRepository(AuthContext context)
        {
            _context = context;
        }

        public async Task<Users> GetUserByLoginAndPasswordAsync(string login, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return user;
            }
            return null;
        }
    }
}
