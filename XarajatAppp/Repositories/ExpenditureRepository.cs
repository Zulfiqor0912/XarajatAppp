using XarajatAppp.Data;

namespace XarajatAppp.Repositories;

public class ExpenditureRepository : IExpenditureRepository
{
    private decimal _amount;
    private List<Expenditure> expenditures = new List<Expenditure>();
    public async Task AddCost(Expenditure expenditure)
    {
        _amount += expenditure.Amount;
       expenditures.Add(expenditure);
    }

    public async Task<UsertCost> Calculate()
    {
        
    }
}
