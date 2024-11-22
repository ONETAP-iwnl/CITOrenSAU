using AuthService.Model;

namespace AuthService.Interface
{
    // Интерфейс для репозитория пользователей
    public interface IUserRepository
    {
        Task<Users> GetUserByLoginAndPasswordAsync(string login, string password);
    }
}
