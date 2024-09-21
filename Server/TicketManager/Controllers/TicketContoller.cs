using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketManager.Context;
using TicketManager.Model;

namespace TicketManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketContoller: ControllerBase
    {
        private readonly TicketContext _ticketContext;

        public TicketContoller(TicketContext ticketContext)
        {
            _ticketContext = ticketContext;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllTicket() //получение всех тикетов
        {
            var ticket = await _ticketContext.Tickets.ToListAsync();
            return Ok(ticket);
        }

        [HttpGet("{numberticket}")]
        public async Task<IActionResult> GetTicket(int ID_Ticket) //получение тикета по номеру
        {
            var ticket = await _ticketContext.Tickets.FindAsync(ID_Ticket);
            if(ticket == null)
            {
                return NotFound();

            }
            return Ok(ticket);
        }

        [HttpGet("{statusTicket}")]
        public async Task<IActionResult> GetStatusTicket(string statusTicket) //получение статуса тикета
        {
            var ticket = await _ticketContext.Tickets.Where(t => t.Status == statusTicket).ToListAsync();
            if(ticket == null || !ticket.Any())
            {
                return NotFound();
            }
            return Ok(ticket);
        }


        [HttpPost("createTicket")]
        public async Task<IActionResult> CreateNewTicket([FromBody] Ticket newTicket)
        {
            if (newTicket == null)
            {
                return BadRequest("Invalid ticket data.");
            } 

            try
            {
                newTicket.DateOfCreation = DateTime.UtcNow;
                _ticketContext.Tickets.Add(newTicket);
                await _ticketContext.SaveChangesAsync();
                return Ok(newTicket);
            }
            catch (Exception ex)
            {
                // Логирование исключения
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
