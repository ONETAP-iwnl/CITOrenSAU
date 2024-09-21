using Microsoft.EntityFrameworkCore;
using TicketManager.Model;


namespace TicketManager.Context
{
    public class TicketContext: DbContext
    {
        public TicketContext(DbContextOptions<TicketContext> options) : base(options) { }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
