using UserService.Model;

namespace UserService.Interface
{
    public interface IUserFactory
{
    IUserRepository CreateUserRepository();
    IUserService CreateUserService(IUserRepository userRepository);
}

}