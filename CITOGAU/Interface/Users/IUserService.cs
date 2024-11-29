using CITOGAU.Classes.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CITOGAU.Interface.Users
{
    public interface IUserService //ISP
    {
        Task<List<UserResponse>> GetAllUserAsync();
    }
}
