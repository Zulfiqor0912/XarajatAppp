using System;
using System.Collections.Generic;
using System.Text;
using XarajatAppp.Data;
using XarajatAppp.Exensions;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace XarajatAppp.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        public Message message = new Message();
        private UserRepository userRepository;
        public List<Team> teams { get; set; } = new List<Team>();
        public List<User> teamUsers { get; set; }
        private static readonly string PathT = System.IO.Path.Combine(AppContext.BaseDirectory, "teams.json");
        private static readonly string PathTU = System.IO.Path.Combine(AppContext.BaseDirectory, "teamUsers.json");
        public TeamRepository(UserRepository userRepository) {
            if (!File.Exists(PathT)) teams = new List<Team>();
            if (!File.Exists(PathTU)) teamUsers = new List<User>();
                
            this.userRepository = userRepository;
        }
        public async Task<bool> AddTeam(string teamName, string username, string password)
        {
            var t = teams.Find(t => t.Name == teamName);
            if (t != null)
            {
                var hasher = new PasswordHasher<object>();
                var result = hasher.VerifyHashedPassword(null, t.PasswordHash, password);

                if (result == PasswordVerificationResult.Success)
                {
                    var user = await userRepository.GetUserByName(username);
                    if (user != null)
                    {
                        teamUsers.Add(user);
                        message.ShowMessage($"{teamName} guruhiga {username} foydalanuvchi qo'shildi");
                        return true;
                    }
                    else
                    {
                        message.ShowMessage("Foydalanuchi topilmadi");
                        return false;
                    }
                }
                else { message.ShowMessage("Parol noto'g'ri"); return false; }
                
            }
            else 
            {
                message.ShowMessage("Guruh topilmadi");
                return false;
            }
                 
        }
        public async Task<bool> CreateTeam(string teamName, string password)
        {
            if (!File.Exists(PathT)) teams = new List<Team>();
            if (teams.Find(t => t.Name == teamName) is null)
            {
                var hasher = new PasswordHasher<object>();
                string hash = hasher.HashPassword(null, password);

                var team = new Team
                {
                    Id = Guid.NewGuid(),
                    Name = teamName,
                    PasswordHash = hash
                };
                teams = await GetAllTeam();
                if (teams.Contains(team))
                    Console.WriteLine("Bunday guruh mavjud");
                else
                {
                    teams.Add(team);
                    var json = JsonSerializer.Serialize(teams);
                    await File.WriteAllTextAsync(PathT, json);
                    Console.WriteLine("Guruh yaratildi");
                    return true;
                }
                return false;
            }
            else { message.ShowMessage("Bunday nomli guruh mavjud"); return false; }
            
        }
        public async Task<Team> GetTeamByName(string teamName)
        {
            //foreach (var item in teams)
            //{
            //    Console.WriteLine(item);
            //}
            var team = teams.Find(t => t.Name == teamName);
            return team;
        }

        public async Task<List<Team>> GetAllTeam()
        {
            if (!File.Exists(PathT))
                return new List<Team>();
            string json = await File.ReadAllTextAsync(PathT);
            if (string.IsNullOrWhiteSpace(json))
                return new List<Team>();

            teams = JsonSerializer.Deserialize<List<Team>>(json) ?? new List<Team>();
            return teams;
        }
    }
}
