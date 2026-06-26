using XarajatAppp.Data;

namespace XarajatAppp.Repositories;

public interface IUserRepository
{
    public Task<User> Register(User user);
    public Task<bool> Login (User user);
}
