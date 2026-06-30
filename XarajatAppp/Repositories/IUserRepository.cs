using XarajatAppp.Data;

namespace XarajatAppp.Repositories;

public interface IUserRepository
{
    public Task<bool> Register(User user);
    public Task<bool> Login (User user);
}
