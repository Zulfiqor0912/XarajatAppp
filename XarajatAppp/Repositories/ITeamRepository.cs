using XarajatAppp.Data;

namespace XarajatAppp.Repositories;

public interface ITeamRepository
{
    public Task<Team> CreateTeam(Team team);
}
