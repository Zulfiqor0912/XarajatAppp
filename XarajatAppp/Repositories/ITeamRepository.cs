using XarajatAppp.Data;

namespace XarajatAppp.Repositories;

public interface ITeamRepository
{
    public Task CreateTeam(Team team);
    public Task AddTeam(string password, Guid userId, string teamName);
    public Task<Team> GetTeamByName(string teamName);
}
