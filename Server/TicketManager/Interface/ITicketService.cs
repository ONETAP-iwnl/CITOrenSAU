using TicketManager.Model;

namespace TicketManager.Interface
{
    public interface ITicketService
    {
        Task<IEnumerable<Ticket>> GetAllTicketsAsync();
        Task<Ticket> GetTicketByIdAsync(int ticketId);
        Task<IEnumerable<Ticket>> GetTicketsByStatusAsync(string status);
        Task<Ticket> CreateTicketAsync(Ticket newTicket);
    }
}
