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
    }
}