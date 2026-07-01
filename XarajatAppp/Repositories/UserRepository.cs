using System.Text.Json;
using XarajatAppp.Data;
using XarajatAppp.Exensions;

namespace XarajatAppp.Repositories;

public class UserRepository() : IUserRepository
{
    public List<User> users { get; set; } = new List<User>();
    public Message message = new Message();
    

    public async Task<bool> Login(string username)
    {
        var user = users.Find(u => u.Username == username);
        return user == null ? false : true;
    }

    public async Task<bool> Register(string username, string fullname)
    {
        var user = new User
        {
            Id = new Guid(),
            Username = username,
            Fullname = fullname,
            CreatedDate = DateTime.Now
        };
        if (users.Contains(user))
            return false;
        else
        {
            users.Add(user);
            return true;
        }
    }
    public Task<User> GetUserById(Guid userId)
    {
        var user = users.FirstOrDefault(u => u.Id == userId);
        return Task.FromResult(user);
    }
}
