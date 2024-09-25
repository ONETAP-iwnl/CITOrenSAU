using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CITOGAU.ApiContext
{
    public class UserResponse
    {
        public int ID_User { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string FIO { get; set; }

    }
}
