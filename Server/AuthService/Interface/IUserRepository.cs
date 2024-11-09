using AuthService.Model;

namespace AuthService.Interface
{
    // Интерфейс для репозитория пользователей
    public interface IUserRepository
    {
        Task<User> GetUserByLoginAndPasswordAsync(string login, string password);
    }
}
