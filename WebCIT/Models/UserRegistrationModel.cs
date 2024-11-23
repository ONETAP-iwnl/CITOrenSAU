using System.ComponentModel.DataAnnotations;

namespace WebCIT.Models
{
    public class UserRegistrationModel
    {
        [Required(ErrorMessage = "Введите логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Введите email")]
        [EmailAddress(ErrorMessage = "Введите корректный email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Выберите роль")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Введите ФИО")]
        public string FIO { get; set; }

        [Required(ErrorMessage = "Введите номер телефона")]
        [Phone(ErrorMessage = "Введите корректный номер телефона")]
        public string NumberPhone { get; set; }

        public string? PasswordHash { get; set; }
    }
}
