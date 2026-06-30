namespace XarajatAppp.Data;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Fullname { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
}
