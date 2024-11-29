using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CITOGAU.Classes.Executors
{
    public class Executors
    {
        [Key]
        public int ID_Executor { get; set; }
        public int ID_User { get; set; }
    }
}
