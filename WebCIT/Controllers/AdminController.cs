using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebCIT.Api.TicketService;
using WebCIT.Classed.Ticket;
using WebCIT.Models;

namespace WebCIT.Controllers
{
    public class AdminController : Controller
    {
        private readonly TicketService _ticketService;

        public AdminController()
        {
            _ticketService = new TicketService("https://26.191.182.183:7215");
        }

        // Метод для отображения страницы с тикетами
        public async Task<IActionResult> AdminDashboard()
        {
            var tickets = await _ticketService.GetAllTicket();
            return View(tickets);
        }
    }
}
