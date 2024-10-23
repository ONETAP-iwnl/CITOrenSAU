using Microsoft.EntityFrameworkCore;
using UserService.Model;

namespace UserService.Context
{
    public class UserContext: DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }
}
