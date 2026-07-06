using XarajatAppp.Data;

namespace XarajatAppp.Repositories;

public interface IExpenditureRepository
{
    public Task AddCost(string username, string fullname, decimal amount, string description);
    public Task<List<Expenditure>> ShowAllExpenditure(string teamName);
    public Task<List<UsertCost>> Calculate(string teamName);
}
