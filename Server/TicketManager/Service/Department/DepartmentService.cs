using TicketManager.Interface;
using TicketManager.Interface.Department;
using TicketManager.Model.Department;

namespace TicketManager.Service.Department
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<Model.Department.Department>> GetAllDepartmentAsync()
        {
            return await _departmentRepository.GetAllDepartmentAsync();
        }

        public async Task<IEnumerable<DepartmentType>> GetAllDepartmentTypeAsync()
        {
            return await _departmentRepository.GetAllDepartmentTypeAsync();
        }
    }
}