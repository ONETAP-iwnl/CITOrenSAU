using Microsoft.EntityFrameworkCore;
using TicketManager.Context;
using TicketManager.Interface;
using TicketManager.Interface.Department;

namespace TicketManager.Model.Department
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DepartmentContext _context;

        public DepartmentRepository(DepartmentContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Department>> GetAllDepartmentAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<IEnumerable<DepartmentType>> GetAllDepartmentTypeAsync()
        {
            return await _context.DepartmentTypes.ToListAsync();
        }
    }
}