using System.ComponentModel.DataAnnotations;

namespace TicketManager.Model.RequestType
{
    public class RequestTypes
    {
        [Key]
        public int ID_RequestType { get; set; }
        public string RequestTypeName { get; set; }
        
    }
}