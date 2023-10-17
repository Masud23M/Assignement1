namespace Domain.Models;

public class User
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Username { get; set; }
    public object PasswordHash { get; set; }
    public string Salt { get; set; }
    public string Id { get; set; }
}