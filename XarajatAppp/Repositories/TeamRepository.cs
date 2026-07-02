using System;
using System.Collections.Generic;
using System.Text;
using XarajatAppp.Data;
using XarajatAppp.Exensions;
using Microsoft.AspNetCore.Identity;

namespace XarajatAppp.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        public Message message = new Message();
        private UserRepository userRepository;
        public List<Team> teams { get; set; } = new List<Team>();
        public List<User> teamUsers { get; set; }
        public TeamRepository(UserRepository userRepository) {
            teamUsers = new List<User>();
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
                    var user = userRepository.GetUserByName(username);
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
        public async Task CreateTeam(string teamName, string password)
        {
            if (teams.Find(t => t.Name != teamName) is null)
            {
                var hasher = new PasswordHasher<object>();
                string hash = hasher.HashPassword(null, password);

                var team = new Team
                {
                    Id = Guid.NewGuid(),
                    Name = teamName,
                    PasswordHash = hash
                };

                teams.Add(team);
                Console.WriteLine("Guruh yaratildi");
            }
            else { message.ShowMessage("Bunday nomli guruh mavjud"); }
            
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
    }
}
