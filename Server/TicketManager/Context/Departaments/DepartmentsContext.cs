using Microsoft.EntityFrameworkCore;
using TicketManager.Model.Department;

namespace TicketManager.Context
{
    public class DepartmentContext: DbContext
    {
        public DepartmentContext(DbContextOptions<DepartmentContext> options) : base(options) { }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentType> DepartmentTypes {get; set; }
    }
}
