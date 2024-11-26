using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CITOGAU.Classes.Tickets
{
    public class Ticket
    {
        public int ID_Ticket { get; set; }
        public int ID_Department { get; set; }
        public DateTime? DateOfCreation { get; set; }
        public DateTime? DateOfCompletion { get; set; }
        public string Status { get; set; }
        public int Author { get; set; }
        public int? Executor { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public string AuthorName { get; set; }
        public string ExecutorName { get; set; }
    }
}
