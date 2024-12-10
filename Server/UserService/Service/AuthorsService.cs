using UserService.Interface;
using UserService.Model;

namespace UserService.Service
{
    public class AuthorsService: IAuthorsService
    {
        private readonly IAuthorsRepository _authorsRepository;
        public AuthorsService(IAuthorsRepository authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }

        public async Task<IEnumerable<Authors>> GetAllAuthorsAsync()
        {
            return await _authorsRepository.GetAllAuthorsAsync();
        }

        public async Task<Authors> GetAuthorsByUserIdAsync(int userId)
        {
            return await _authorsRepository.GetAuthorsByUserIdAsync(userId);
        }
    }
}