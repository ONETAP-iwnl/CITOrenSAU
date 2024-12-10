using Microsoft.AspNetCore.Mvc;
using TicketManager.Interface;
using TicketManager.Interface.Department;

namespace TicketManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController: ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("allDepartment")]
        public async Task<IActionResult> GetAllDepartmentAsync()
        {
            var depart = await _departmentService.GetAllDepartmentAsync();
            return Ok(depart);
        }

        [HttpGet("allDepartmentType")]
        public async Task<IActionResult> GetAllDepartmentTypeAsync()
        {
            var departType = await _departmentService.GetAllDepartmentTypeAsync();
            return Ok(departType);
        }
    }
}