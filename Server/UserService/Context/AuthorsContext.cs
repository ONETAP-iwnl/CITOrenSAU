using Microsoft.EntityFrameworkCore;
using UserService.Model;

namespace UserService.Context
{
    public class AuthorsContext: DbContext
    {
#pragma warning disable CS8618 
        public AuthorsContext(DbContextOptions<AuthorsContext> options) : base(options) { }
#pragma warning restore CS8618
        public DbSet<Authors> Authors { get; set; }
    }
}