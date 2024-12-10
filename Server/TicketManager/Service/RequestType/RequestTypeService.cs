using TicketManager.Interface.RequestType;
using TicketManager.Model.RequestType;

namespace TicketManager.Service.RequestType
{
    public class RequestTypeService : IRequestTypeService
    {
        private readonly IRequestTypeRepository _requestRepository;

        public RequestTypeService(IRequestTypeRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public async Task<IEnumerable<RequestTypes>> GetAllRequestsAsync()
        {
            return await _requestRepository.GetAllRequestsAsync();
        }
    }
}