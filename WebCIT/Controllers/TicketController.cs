using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.Sockets;
using WebCIT.Api.AuthService;
using WebCIT.Api.TicketService;
using WebCIT.Classed.Ticket;

namespace WebCIT.Controllers
{
    public class TicketController: Controller
    {
        private readonly TicketService _ticketService;

        public TicketController()
        {
            _ticketService = new TicketService("https://26.240.38.124:7215");
        }

        public async Task<IActionResult> Details(int ID_Ticket)
        {
            Console.WriteLine($"Requesting ticket with ID: {ID_Ticket}");
            var ticket = await _ticketService.GetTicket(ID_Ticket);
            if (ticket == null)
            {
                Console.WriteLine($"Ticket with ID {ID_Ticket} not found.");
                return NotFound();
            }
            return View(ticket);
        }


        [HttpGet]
        public async Task<IActionResult> AdminDashboard(string searchQuery, string searchType, string searchStatus)
        {
            var ticket = await _ticketService.GetAllTicket();

<<<<<<< HEAD

            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.ToLower();
                ticket = ticket.Where(t =>
                    t.AudienceNumber.ToLower().Contains(searchQuery) ||
                    t.BuildingNumber.ToLower().Contains(searchQuery)
                ).ToList();
            }


            if (!string.IsNullOrEmpty(searchType))
            {
                searchType = searchType.ToLower();
                ticket = ticket.Where(t =>
                t.Type.Equals(searchType, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
=======
            //---------------
            //Нужно исправить, т.к. в бд нет атрибутов AudienceNumber и BuildingNumber
            //

            //// Если запрос поиска не пустой, фильтруем тикеты
            //if (!string.IsNullOrEmpty(searchQuery))
            //{
            //    searchQuery = searchQuery.ToLower();
            //    ticket = ticket.Where(t =>
            //        t.AudienceNumber.ToLower().Contains(searchQuery) ||
            //        t.BuildingNumber.ToLower().Contains(searchQuery)
            //    ).ToList();
            //}


            // нужно исправить, т.к. в бд атрибут Type теперь int, нужно сделать выборку

            // Поиск тикетов по типу
            //if (!string.IsNullOrEmpty(searchType))
            //{
            //    searchType = searchType.ToLower();
            //    ticket = ticket.Where(t =>
            //    t.Type.Equals(searchType, StringComparison.OrdinalIgnoreCase))
            //        .ToList();
            //}
>>>>>>> b9e76ddf6d1a8a8c82eb0f46ddb1964e23a8417e


            if (!string.IsNullOrEmpty(searchStatus))
            {
                ticket = ticket
                    .Where(t => t.Status.Equals(searchStatus, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return View("~/Views/Admin/AdminDashboard.cshtml", ticket);
        }

        [HttpPost]
        public async  Task<IActionResult> RejectTicket(int id)
        {
            var ticket = await _ticketService.GetAllTicket();

            if (ticket == null)
            {
                return NotFound();
            }

            var existingTicket = ticket.FirstOrDefault(t => t.ID_Ticket == id);
            existingTicket.Status = "Отклонена";
            await _ticketService.UpdateStatus(existingTicket);
            return View("~/Views/Admin/AdminDashboard.cshtml", ticket);
        }

    }
}
