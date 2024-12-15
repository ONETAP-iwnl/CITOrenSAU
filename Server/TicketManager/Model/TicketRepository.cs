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

        public async Task<IEnumerable<Ticket>> GetTicketsByStatusAsync(int statusId)
        {
            return await _context.Tickets.Where(t => t.Status == statusId).ToListAsync();
        }

        public async Task<Ticket> CreateTicketAsync(Ticket newTicket)
        {
            _context.Tickets.Add(newTicket);
            await _context.SaveChangesAsync();
            return newTicket;
        }

        public async Task<bool> UpdateTicketStatusAsync(int ticketId, int statusId)
        {
            var ticket = await _context.Tickets.FindAsync(ticketId);
            if (ticket == null)
            {
                return false;
            }

            ticket.Status = statusId;
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AssignExecutorToTicketAsync(int ticketId, int executorId)
        {
            var ticket = await _context.Tickets.FindAsync(ticketId);
            if (ticket == null)
            {
                return false;
            }

            ticket.Executor = executorId;
            ticket.Status = 3;
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
