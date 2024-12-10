using UserService.Model;

namespace UserService.Interface
{
    public interface IAuthorsService
    {
        Task<IEnumerable<Authors>> GetAllAuthorsAsync();
        Task<Authors> GetAuthorsByUserIdAsync(int userId);
    }
}