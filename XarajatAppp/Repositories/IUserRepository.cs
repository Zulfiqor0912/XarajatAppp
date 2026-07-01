using XarajatAppp.Data;

namespace XarajatAppp.Repositories;

public interface IUserRepository
{
    public Task<bool> Register(string username, string fullname);
    public Task<bool> Login (string username);
}
