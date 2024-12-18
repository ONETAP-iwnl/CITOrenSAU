using TicketManager.Model;

namespace TicketManager.Interface
{
    public interface ITicketService
    {
        Task<IEnumerable<Ticket>> GetAllTicketsAsync();
        Task<Ticket> GetTicketByIdAsync(int ticketId);
        Task<IEnumerable<Ticket>> GetTicketsByStatusAsync(int statusId);
        Task<Ticket> CreateTicketAsync(Ticket newTicket);
        Task<bool> UpdateTicketStatusAsync(int ticketId, int statusId);
        Task<bool> AssignExecutorToTicketAsync(int ticketId, int executorId);
        Task<bool> UpdateTicketComplitionDateAsync(int ticketId, DateTime? completionDate);
    }
}
