using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CITOGAU.Classes.Department
{
    public class DepartmentTypes
    {
        [Key]
        public int ID_DepartmentType { get; set; }
        public string DepartmentTypeName { get; set; }
    }
}
