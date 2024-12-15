using TicketManager.Interface;
using TicketManager.Model;

namespace TicketManager.Service
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            return await _ticketRepository.GetAllTicketsAsync();
        }

        public async Task<Ticket> GetTicketByIdAsync(int ticketId)
        {
            return await _ticketRepository.GetTicketByIdAsync(ticketId);
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByStatusAsync(int statusId)
        {
            return await _ticketRepository.GetTicketsByStatusAsync(statusId);
        }

        public async Task<Ticket> CreateTicketAsync(Ticket newTicket)
        {
            newTicket.DateOfCreation = DateTime.UtcNow;
            return await _ticketRepository.CreateTicketAsync(newTicket);
        }

        public async Task<bool> UpdateTicketStatusAsync(int ticketId, int statusId)
        {
            return await _ticketRepository.UpdateTicketStatusAsync(ticketId, statusId);
        }
        public async Task<bool> AssignExecutorToTicketAsync(int ticketId, int executorId)
        {
            return await _ticketRepository.AssignExecutorToTicketAsync(ticketId, executorId);
        }
    }
}
