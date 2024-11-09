using AuthService.DTO_Class;

namespace AuthService.Interface
{
    // Интерфейс для сервиса аутентификации
    public interface IAuthService
    {
        Task<UserResponse> AuthenticateAsync(LoginRequest loginRequest);
    }
}
