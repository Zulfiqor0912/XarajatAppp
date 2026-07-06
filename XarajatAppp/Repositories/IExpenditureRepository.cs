using XarajatAppp.Data;

namespace XarajatAppp.Repositories;

public interface IExpenditureRepository
{
    public Task AddCost(string username, string fullname, decimal amount, string teamname, string description);
    public Task<List<Expenditure>> GetAllExpenditures(string teamName);
    public Task<List<UsertCost>> Calculate(string teamName);
}
