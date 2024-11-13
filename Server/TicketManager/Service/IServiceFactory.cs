using Microsoft.Extensions.DependencyInjection;
using TicketManager.Interface;


namespace TicketManager.Service
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ServiceFactory(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public ITicketService CreateTicketService()
        {
            var scope = _serviceScopeFactory.CreateScope();
            return scope.ServiceProvider.GetRequiredService<ITicketService>();
        }
    }
}
