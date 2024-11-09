namespace AuthService.DTO_Class
{
    // DTO для запроса на вход в систему
    public class LoginRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
