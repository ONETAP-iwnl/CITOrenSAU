using Microsoft.EntityFrameworkCore;
using TicketManager.Context;
using TicketManager.Interface;
using TicketManager.Interface.RequestType;

namespace TicketManager.Model.RequestType
{
    public class RequestTypeRepository: IRequestTypeRepository
    {
        private readonly RequestTypeContext _context;
        
        public RequestTypeRepository(RequestTypeContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RequestTypes>> GetAllRequestsAsync()
        {
            return await _context.RequestTypes.ToListAsync();
        }
    }
}