using TicketManager.Model;
using TicketManager.Model.Department;

namespace TicketManager.Interface.Department
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Model.Department.Department>> GetAllDepartmentAsync();
        Task<IEnumerable<DepartmentType>> GetAllDepartmentTypeAsync();
    }
}