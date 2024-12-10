using UserService.Model;

namespace UserService.Interface
{
    public interface IAuthorsRepository
    {
        Task<IEnumerable<Authors>> GetAllAuthorsAsync();
        Task<Authors> GetAuthorsByUserIdAsync(int userId);
    }
}