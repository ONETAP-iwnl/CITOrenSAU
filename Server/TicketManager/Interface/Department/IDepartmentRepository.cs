using TicketManager.Model;
using TicketManager.Model.Department;

namespace TicketManager.Interface
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Model.Department.Department>> GetAllDepartmentAsync();
        Task<IEnumerable<DepartmentType>> GetAllDepartmentTypeAsync();
    }
}
