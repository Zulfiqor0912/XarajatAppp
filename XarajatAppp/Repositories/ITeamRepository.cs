using XarajatAppp.Data;

namespace XarajatAppp.Repositories;

public interface ITeamRepository
{
    public Task CreateTeam(string teamName, string password);
    public Task AddTeam(string password, string username, string teamName);
    public Task<Team> GetTeamByName(string teamName);
}
