using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CITOGAU.Classes.Department
{
    public class Department
    {
        [Key]
        public int ID_Department { get; set; }
        public string DepartmentName { get; set; }
    }
}
