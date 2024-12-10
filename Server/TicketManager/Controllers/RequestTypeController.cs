using Microsoft.AspNetCore.Mvc;
using TicketManager.Interface;
using TicketManager.Interface.RequestType;

namespace TicketManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RequestTypeController: ControllerBase
    {
        private readonly IRequestTypeService _requestService;

        public RequestTypeController(IRequestTypeService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet("allRequest")]
        public async Task<IActionResult> GetAllRequestsAsync()
        {
            var depart = await _requestService.GetAllRequestsAsync();
            return Ok(depart);
        }
    }
}
