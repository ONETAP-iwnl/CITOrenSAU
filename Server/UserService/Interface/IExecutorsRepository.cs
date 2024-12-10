using UserService.Model;

namespace UserService.Interface
{
    public interface IExecutorsRepository
    {
        Task<IEnumerable<Executors>> GetAllExecutorsAsync();
    }
}