using XarajatAppp.Data;
using XarajatAppp.Exensions;

namespace XarajatAppp.Repositories;

public class ExpenditureRepository() : IExpenditureRepository
{
    private TeamRepository teamRepo = new TeamRepository();
    private decimal _amount;
    private List<Expenditure> expenditures = new List<Expenditure>();
    private List<UsertCost> userCosts = new List<UsertCost>();
    public async Task AddCost(string username, string fullname, decimal amount, string description)
    {
        _amount += amount;
        var expenditure = new Expenditure
        {
            Id = new Guid(),
            Username = username,
            Fullname = fullname,
            Amount = amount,
            Description = description,
            CreatedDate = DateTime.Now
        };

       expenditures.Add(expenditure);
    }

    public List<UsertCost> Calculate(string teamName)
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
