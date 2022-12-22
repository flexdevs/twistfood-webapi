namespace TwistFood.Api.Models;

public class Admin
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;    
    public string Salt { get; set; } = string.Empty;
}
