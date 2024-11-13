using Microsoft.EntityFrameworkCore;
using TicketManager.Context;
using TicketManager.Interface;

namespace TicketManager.Model
{
    public class TicketRepository : ITicketRepository
    {
        private readonly TicketContext _context;

        public TicketRepository(TicketContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            return await _context.Tickets.ToListAsync();
        }

        public async Task<Ticket> GetTicketByIdAsync(int ticketId)
        {
            return await _context.Tickets.FindAsync(ticketId);
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByStatusAsync(string status)
        {
            return await _context.Tickets.Where(t => t.Status == status).ToListAsync();
        }

        public async Task<Ticket> CreateTicketAsync(Ticket newTicket)
        {
            _context.Tickets.Add(newTicket);
            await _context.SaveChangesAsync();
            return newTicket;
        }
    }

}
