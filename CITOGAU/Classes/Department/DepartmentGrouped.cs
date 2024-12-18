using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CITOGAU.Classes.Department
{
    public class DepartmentGrouped
    {
        public DepartmentTypes DepartmentType { get; set; }
        public ObservableCollection<Department> Departments { get; set; }
    }
}
