using UserService.Interface;
using UserService.Model;

namespace UserService.Service
{
    public class ExecutorsService: IExecutorsService
    {
        private readonly IExecutorsRepository _executorRepository;

        public ExecutorsService(IExecutorsRepository executorsRepository)
        {
            _executorRepository = executorsRepository;
        }

        public async Task<IEnumerable<Executors>> GetAllExecutorsAsync()
        {
            return await _executorRepository.GetAllExecutorsAsync();
        }

        public async Task<Executors> GetExecutorsByUserIdAsync(int userId)
        {
            return await _executorRepository.GetExecutorsByUserIdAsync(userId);
        }
    }
}