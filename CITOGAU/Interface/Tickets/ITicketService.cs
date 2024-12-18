using CITOGAU.Classes.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CITOGAU.Interface.Tickets
{
    public interface ITicketService //ISP
    {
        Task<List<Ticket>> GetAllTicketsAsync();
        Task<Ticket> CreateTicketAsync(Ticket newTicket);
        Task UpdateTicketAsync(Ticket ticket);
        Task<bool> UpdateTicketStatusAsync(int id, int statusId);
        Task<bool> AssignExecutorToTicketAsync(int id, int executorId);

        Task<bool> UpdateTicketCompletionDateAsync(int ticketId, DateTime? completionDate);
    }
}
