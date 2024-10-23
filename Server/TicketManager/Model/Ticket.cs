using System.ComponentModel.DataAnnotations;

namespace TicketManager.Model
{
    public class Ticket
    {
        [Key]
        public int ID_Ticket { get; set; }
        public string AudienceNumber { get; set; }
        public string BuildingNumber { get; set; }
        public DateTime? DateOfCreation { get; set; }  // Nullable DateTime
        public DateTime? DateOfCompletion { get; set; }  // Nullable DateTime
        public string Status { get; set; }
        public int? Author { get; set; }
        public int? Executor { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string? NumberPhone { get; set; }
    }
}
