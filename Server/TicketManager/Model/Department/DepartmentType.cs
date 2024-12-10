using System.ComponentModel.DataAnnotations;

namespace TicketManager.Model.Department
{
    public class DepartmentType
    {
        [Key]
        public int ID_DepartmentType { get; set; }
        public string DepartmentTypeName { get; set; }
    }
}