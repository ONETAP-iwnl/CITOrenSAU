﻿using TicketManager.Model;

namespace TicketManager.Interface
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Ticket>> GetAllTicketsAsync();
        Task<Ticket> GetTicketByIdAsync(int ticketId);
        Task<IEnumerable<Ticket>> GetTicketsByStatusAsync(int statusId);
        Task<Ticket> CreateTicketAsync(Ticket newTicket);
        Task<bool> UpdateTicketStatusAsync(int ticketId, int statusId);
    }
}
