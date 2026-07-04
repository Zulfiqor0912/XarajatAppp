using System.Text.Json;
using XarajatAppp.Data;
using XarajatAppp.Exensions;

namespace XarajatAppp.Repositories;

public class UserRepository : IUserRepository
{
    public List<User> users { get; set; }
    public Message message = new Message();
    private static readonly string Path = System.IO.Path.Combine(AppContext.BaseDirectory, "users.json");

    public UserRepository()
    {
        if (!File.Exists(Path))
            users = new List<User>();
    }
    

    public async Task<bool> Login(string username)
    {
        var user = users.Find(u => u.Username == username);
        return user == null ? false : true;
    }

    public async Task<bool> Register(string username, string fullname)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = username,
            Fullname = fullname,
            CreatedDate = DateTime.Now
        };

        users = await GetAllUsers();

        if (users.Contains(user))
            return false;
        else
        {
            users.Add(user);
            var json = JsonSerializer.Serialize(users);
            File.WriteAllText(Path, json);
            return true;
        }
    }
    public  async Task<User> GetUserByName(string username)
    {
        var us = await GetAllUsers();
        var user = us.FirstOrDefault(u => u.Username == username);
        return user;
    }

    public async Task<List<User>> GetAllUsers()
    {
        if (!File.Exists(Path))
            return new List<User>();
        string json = await File.ReadAllTextAsync(Path);
        if (string.IsNullOrWhiteSpace(json))
            return new List<User>();
        users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        return users;
    }
}
