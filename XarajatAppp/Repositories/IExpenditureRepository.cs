using XarajatAppp.Data;

namespace XarajatAppp.Repositories;

public interface IExpenditureRepository
{
    public Task AddCost(Expenditure expenditure);
    public Task<List<UsertCost>> Calculate(string teamName);
}
