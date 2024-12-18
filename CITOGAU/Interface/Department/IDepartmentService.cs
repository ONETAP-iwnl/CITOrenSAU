using CITOGAU.Classes.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CITOGAU.Interface.Department
{
    public interface IDepartmentService //ISP
    {
        Task<List<Classes.Department.Department>> GetAllDepartmentAsync();
        Task<List<Classes.Department.DepartmentTypes>> GetAllDepartmentTypeAsync();
    }
}
