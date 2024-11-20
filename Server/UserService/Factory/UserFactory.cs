using UserService.Context;
using UserService.Interface;
using UserService.Model;
using UserService.Repository;

public class UserFactory : IUserFactory
{
    private readonly UserContext _context;

    public UserFactory(UserContext context)
    {
        _context = context;
    }

    public IUserRepository CreateUserRepository()
    {
        return new UserRepository(_context);
    }

    public IUserService CreateUserService(IUserRepository userRepository)
    {
        return new UserService.Service.UserService(userRepository);
    }
}