﻿using System.ComponentModel.DataAnnotations;

namespace UserService.Model
{
    public class User
    {
        [Key]
        public int ID_User { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public string Role { get; set; }

        public string FIO { get; set; }
    }
}
