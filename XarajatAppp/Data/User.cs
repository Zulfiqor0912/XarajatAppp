namespace XarajatAppp.Data;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
}
