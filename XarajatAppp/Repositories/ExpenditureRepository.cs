using System.Text.Json;
using XarajatAppp.Data;
using XarajatAppp.Exensions;

namespace XarajatAppp.Repositories;

public class ExpenditureRepository : IExpenditureRepository
{
    private TeamRepository teamRepository;
    private decimal _amount;
    private List<Expenditure> expenditures;
    private List<UsertCost> userCosts;
    private Message message = new Message();

    private static readonly string PathE = System.IO.Path.Combine(AppContext.BaseDirectory, "expenditure.json");
    private static readonly string PathUC = System.IO.Path.Combine(AppContext.BaseDirectory, "usercost.json");

    public ExpenditureRepository(TeamRepository teamRepository)
    {
        if (!File.Exists(PathE)) expenditures = new List<Expenditure>();
        if (!File.Exists(PathUC)) userCosts = new List<UsertCost>();
        this.teamRepository = teamRepository;

    }
    public async Task AddCost(string username, string fullname, decimal amount, string teamname, string description)
    {
        
        if (!File.Exists(PathUC)) userCosts = new List<UsertCost>();

        expenditures = await GetAllExpenditures(teamname);

        _amount += amount;
        var expenditure = new Expenditure
        {
            Id = Guid.NewGuid(),
            Username = username,
            Fullname = fullname,
            Amount = amount,
            Description = description,
            TeamName = teamname,
            CreatedDate = DateTime.Now
        };

        expenditures.Add(expenditure);
        var json = JsonSerializer.Serialize(expenditures);
        await File.WriteAllTextAsync(PathE, json);
        message.ShowMessage("Xarajat saqlandi!");
    }

    public async Task<List<UsertCost>> Calculate(string teamName)
    {
        var team = await teamRepository.GetTeamByName(teamName);
        if (team != null && expenditures.Count != 0)
        {
            
            
            var _costUsers = expenditures
                .GroupBy(x => x.Username)
                .Select(x => new
                {
                    Username = x.Key,
                    TotalAmount = x.Sum(y => y.Amount)
                }).ToList();

            var userCount = _costUsers
                .Select(n => n.Username)
                .Distinct()
                .Count();
            decimal avareageCost = _amount / (decimal)userCount;

            userCosts = _costUsers
                .Select(x => new UsertCost
                {
                    Id = Guid.NewGuid(),
                    Username = x.Username,
                    Fullname = "x.",
                    TotalCost = x.TotalAmount,
                    ToGetMoney = x.TotalAmount >= avareageCost ? (x.TotalAmount - avareageCost) : 0,
                    ToGiveMoney = x.TotalAmount <= avareageCost ? (x.TotalAmount - avareageCost) : 0,
                    TotalCostTeamMoney = _amount
                }).ToList();
            return userCosts;
        }
        else {
            return userCosts;
        }
    }

    public async Task<List<Expenditure>> GetAllExpenditures(string teamName)
    {
        if (!File.Exists(PathE)) return new List<Expenditure>();

        string json = await File.ReadAllTextAsync(PathE);

        if (string.IsNullOrWhiteSpace(json)) return new List<Expenditure>();

        var e = JsonSerializer.Deserialize<List<Expenditure>>(json) ?? new List<Expenditure>();

        return e
            .Where(tn => tn.TeamName == teamName)
            .ToList(); ;
    }
}
