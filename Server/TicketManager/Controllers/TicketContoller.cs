using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketManager.Context;
using TicketManager.Interface;
using TicketManager.Model;

namespace TicketManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController: ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(IServiceFactory serviceFactory)
        {
            _ticketService = serviceFactory.CreateTicketService();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllTickets()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            return Ok(tickets);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTicket(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetTicketsByStatus(string status)
        {
            var tickets = await _ticketService.GetTicketsByStatusAsync(status);
            if (!tickets.Any())
            {
                return NotFound();
            }
            return Ok(tickets);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTicket([FromBody] Ticket newTicket)
        {
            if (newTicket == null)
            {
                return BadRequest("Invalid ticket data.");
            }

            try
            {
                var createdTicket = await _ticketService.CreateTicketAsync(newTicket);
                return Ok(createdTicket);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
