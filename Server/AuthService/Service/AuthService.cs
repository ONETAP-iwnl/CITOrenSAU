using AuthService.DTO_Class;
using AuthService.Interface;

namespace AuthService.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Метод для аутентификации пользователя
        public async Task<UserResponse> AuthenticateAsync(LoginRequest loginRequest)
        {
            var user = await _userRepository.GetUserByLoginAndPasswordAsync(loginRequest.Login, loginRequest.Password);

            if (user == null)
            {
                return null;
            }

            return new UserResponse
            {
                ID_User = user.ID_User,
                Login = user.Login,
                Email = user.Email,
                Role = user.Role,
                FIO = user.FIO
            };
        }
    }
}
