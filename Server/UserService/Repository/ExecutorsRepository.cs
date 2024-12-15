using Microsoft.EntityFrameworkCore;
using UserService.Context;
using UserService.Interface;
using UserService.Model;

namespace UserService.Repository
{
    public class ExecutorsRepository : IExecutorsRepository
    {
        private readonly ExecutorsContext _context;
        public ExecutorsRepository(ExecutorsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Executors>> GetAllExecutorsAsync()
        {
            return await _context.Executors.ToListAsync();
        }
        public async Task<Executors> GetExecutorsByUserIdAsync(int userId)
        {
            return await _context.Executors.FirstOrDefaultAsync(x => x.ID_User == userId);
        }

    }
}