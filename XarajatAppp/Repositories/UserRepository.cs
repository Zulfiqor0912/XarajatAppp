using System.Text.Json;
using XarajatAppp.Data;
using XarajatAppp.Exensions;

namespace XarajatAppp.Repositories;

public class UserRepository() : IUserRepository
{
    public List<User> users { get; set; } = new List<User>();
    public Message message = new Message();
    

    public Task<bool> Login(User user)
    {
        return Task.FromResult(users.Contains(user));
    }

    public Task<bool> Register(User user)
    {
        if (users.Contains(user))
            return Task.FromResult(false);
        else
        {
            users.Add(user);
            return Task.FromResult(true);
        }
    }
    public Task<User> GetUserById(Guid userId)
    {
        var user = users.FirstOrDefault(u => u.Id == userId);
        return Task.FromResult(user);
    }
}
