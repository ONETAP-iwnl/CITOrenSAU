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

         [HttpGet("status/{statusId:int}")]
        public async Task<IActionResult> GetTicketsByStatus(int statusId)
        {
            var tickets = await _ticketService.GetTicketsByStatusAsync(statusId);
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

        [HttpPut("{id:int}/status/{statusId:int}")]
        public async Task<IActionResult> UpdateTicketStatus(int id, int statusId)
        {
            var result = await _ticketService.UpdateTicketStatusAsync(id, statusId);
            if (!result)
            {
                return NotFound("Ticket not found");
            }
            return Ok("Ticket status updated successfully");
        }

        [HttpPut("{id:int}/assign/{executorId:int}")]
        public async Task<IActionResult> AssignExecutorToTicket(int id, int executorId)
        {
            var result = await _ticketService.AssignExecutorToTicketAsync(id, executorId);
            if (!result)
            {
                return NotFound("Ticket not found");
            }
            return Ok("Executor assigned successfully");
        }
    }
}
