﻿namespace AuthService.DTO_Class
{
    // DTO для ответа пользователю
    public class UserResponse
    {
        public int ID_User { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string FIO { get; set; }
        public string NumberPhone { get; set; }
    }
}
