using Microsoft.EntityFrameworkCore;
using UserService.Model;

namespace UserService.Context
{
    public class ExecutorsContext: DbContext
    {
        #pragma warning disable CS8618 
        public ExecutorsContext(DbContextOptions<ExecutorsContext> options) : base(options) { }
        public DbSet<Executors> Executors { get; set; }
    }
}