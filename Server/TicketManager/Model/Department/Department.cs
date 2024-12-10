using System.ComponentModel.DataAnnotations;

namespace TicketManager.Model.Department
{
    public class Department
    {
        [Key]
        public int ID_Department { get; set; }
        public string DepartmentName { get; set; }
        public int ID_DepartmentType { get; set; }
    }
}