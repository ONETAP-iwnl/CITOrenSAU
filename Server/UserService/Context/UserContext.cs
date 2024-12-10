using Microsoft.EntityFrameworkCore;
using UserService.Model;

namespace UserService.Context
{
    public class UserContext: DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Users>().ToTable(tb => tb.HasTrigger("trg_InsertUser"));
        }
    }
}
