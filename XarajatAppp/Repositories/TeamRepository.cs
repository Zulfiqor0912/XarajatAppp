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
        public UserRepository userRepository = new UserRepository();
        public List<Team> teams { get; set; } = new List<Team>();
        public List<User> teamUsers { get; set; } = new List<User>();
        public async Task AddTeam(string password, Guid userId, string teamName)
        {
            var t = teams.Find(t => t.Name == teamName);
            if (t != null)
            {
                var user = await userRepository.GetUserById(userId);
                if (user != null)
                {
                    teamUsers.Add(user);
                }
                else
                {
                    message.ShowMessage("User not found.");
                }
            }
            else
            {
                message.ShowMessage("Team not found."); 
            }
        }
        public Task CreateTeam(Team team)
        { 
            teams.Add(team);
            return Task.CompletedTask;
        }
        public async Task<Team> GetTeamByName(string teamName)
        {
            return teams.Find(t => t.Name == teamName)!;
        }
    }
}
