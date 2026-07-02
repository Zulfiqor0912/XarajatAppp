using XarajatAppp.Data;

namespace XarajatAppp.Repositories;

public interface ITeamRepository
{
    public Task CreateTeam(string teamName, string password);
    public Task<bool> AddTeam(string teamName, string username, string password);
    public Task<Team> GetTeamByName(string teamName);
}
