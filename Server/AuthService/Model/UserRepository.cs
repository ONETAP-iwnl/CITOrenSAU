using AuthService.Context;
using AuthService.Interface;
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

        // Метод для получения пользователя по логину и паролю
        public async Task<User> GetUserByLoginAndPasswordAsync(string login, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Login == login && u.Password == password);
        }


        //нужно сделать хэширование пароля, я пока хз как это сделать
        //помогите

    }
}
