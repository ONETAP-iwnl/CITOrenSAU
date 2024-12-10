using Microsoft.EntityFrameworkCore;
using TicketManager.Model;
using TicketManager.Model.RequestType;


namespace TicketManager.Context
{
    public class RequestTypeContext: DbContext
    {
        public RequestTypeContext(DbContextOptions<RequestTypeContext> options) : base(options) { }
        public DbSet<RequestTypes> RequestTypes { get; set; }
    }
}
