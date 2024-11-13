using System.ComponentModel.DataAnnotations;

namespace TicketManager.Model
{
    public class Ticket
    {
        [Key]
        public int ID_Ticket { get; set; }
        public int ID_Department { get; set; }  // Foreign key to Departments
        public DateTime? DateOfCreation { get; set; }
        public DateTime? DateOfCompletion { get; set; }
        public string Status { get; set; }
        public int? Author { get; set; }
        public int? Executor { get; set; }
        public int Type { get; set; }  // Foreign key to RequestTypes
        public string Description { get; set; }
    }
}
