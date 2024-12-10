using UserService.Model;

namespace UserService.Interface
{
    public interface IExecutorsService
    {
        Task<IEnumerable<Executors>> GetAllExecutorsAsync();
    }
}