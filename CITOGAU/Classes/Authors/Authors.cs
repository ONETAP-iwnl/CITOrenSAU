using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CITOGAU.Classes.Authors
{
    public class Authors
    {
        [Key]
        public int ID_Author { get; set; }
        public int ID_User { get; set; }
    }
}
