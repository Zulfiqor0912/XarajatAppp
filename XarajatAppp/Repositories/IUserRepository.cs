using XarajatAppp.Data;

namespace XarajatAppp.Repositories;

public interface IUserRepository
{
    public Task<bool> Register(string username, string fullname, string password);
    public Task<bool> Login (string username, string password);
    public Task<List<User>> GetAllUsers();
}
