using TicketManager.Model.RequestType;

namespace TicketManager.Interface.RequestType
{
    public interface IRequestTypeService
    {
        Task<IEnumerable<RequestTypes>> GetAllRequestsAsync();
    }
}