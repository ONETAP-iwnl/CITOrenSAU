using TicketManager.Context;
using TicketManager.Model.RequestType;

namespace TicketManager.Interface.RequestType
{
    public interface IRequestTypeRepository
    {
        Task<IEnumerable<RequestTypes>> GetAllRequestsAsync();
    }
}