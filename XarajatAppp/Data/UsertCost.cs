namespace XarajatAppp.Data;

public class UsertCost
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Fullname { get; set; } = string.Empty;
    public decimal TotalCost { get; set; }
    public decimal ToGetMoney { get; set; }
    public decimal ToGiveMoney { get; set; }
    public decimal TotalCostTeamMoney { get; set; }
}
