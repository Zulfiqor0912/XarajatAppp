using System;
using System.Collections.Generic;
using System.Text;
using XarajatAppp.Data;
using XarajatAppp.Exensions;

namespace XarajatAppp.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        public Message message = new Message();
        private UserRepository userRepository;
        public List<Team> teams { get; set; } = new List<Team>();
        public List<User> teamUsers { get; set; }
        public TeamRepository(UserRepository userRepository) {
            this.userRepository = userRepository;
        }
        public async Task AddTeam(string teamName, string username, string password)
        {
            var t = teams.Find(t => t.Name == teamName);
            if (t != null)
            {
                var user = userRepository.GetUserById(username);
                if (user != null)
                {
                    teamUsers.Add(user);
                    message.ShowMessage($"{teamName} guruhiga {username} foydalanuvchi qo'shildi");
                }
                else
                    message.ShowMessage("Foydalanuchi topilmadi");
            }
            else
                message.ShowMessage("Guruh topilmadi"); 
        }
        public async Task CreateTeam(string teamName, string password)
        {
            if (teams.Find(t => t.Name != teamName) is null)
            {
                var team = new Team
                {
                    Id = new Guid(),
                    Name = teamName,
                    PasswordHash = password
                };

                teams.Add(team);
                Console.WriteLine("Guruh yaratildi");
            }
            else { message.ShowMessage("Bunday nomli guruh mavjud"); }
            
        }
        public async Task<Team> GetTeamByName(string teamName)
        {
            return teams.Find(t => t.Name == teamName)!;
        }
    }
}
