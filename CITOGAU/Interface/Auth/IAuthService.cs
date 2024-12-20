using CITOGAU.Classes.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CITOGAU.Interface.Auth
{
    public interface IAuthService
    {
        Task<UserResponse> LoginAsync(string login, string password);
    }
}
