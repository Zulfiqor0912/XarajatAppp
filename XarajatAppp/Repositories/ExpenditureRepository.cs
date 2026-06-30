using XarajatAppp.Data;
using XarajatAppp.Exensions;

namespace XarajatAppp.Repositories;

public class ExpenditureRepository() : IExpenditureRepository
{
    private TeamRepository teamRepo = new TeamRepository();
    private decimal _amount;
    private List<Expenditure> expenditures = new List<Expenditure>();
    private List<UsertCost> userCosts = new List<UsertCost>();
    public async Task AddCost(Expenditure expenditure)
    {
        _amount += expenditure.Amount;
       expenditures.Add(expenditure);
    }

    public async Task<List<UsertCost>> Calculate(string teamName)
    {
        var team = teamRepo.GetTeamByName(teamName);
        if (team != null)
        {
            decimal avareageCost = _amount / (decimal)expenditures.Count;
            var _costUsers = expenditures
                .GroupBy(x => x.Username)
                .Select(x => new
                {
                    Username = x.Key,
                    TotalAmount = x.Sum(y => y.Amount)
                }).ToList();

                userCosts = _costUsers
                .Select(x => new UsertCost
                {
                    Id = new Guid(),
                    Username = x.Username,
                    Fullname = "x.",
                    TotalCost = x.TotalAmount,
                    ToGetMoney = x.TotalAmount >= avareageCost ? 0 : avareageCost - x.TotalAmount,
                    ToGiveMoney = x.TotalAmount <= avareageCost ? avareageCost - x.TotalAmount : 0,
                    TotalCostTeamMoney = _amount
                }).ToList();
            return userCosts;
        }
        else {
            return userCosts;
        }
        
    }
}
