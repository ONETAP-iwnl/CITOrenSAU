namespace TicketManager.Interface
{
    public interface IServiceFactory
    {
        ITicketService CreateTicketService();
    }
}
