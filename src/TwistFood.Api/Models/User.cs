namespace TwistFood.Api.Models;

public class User
{
    public long Id { get; set; }
    public string TelegramId { get; set; } = string.Empty;  
    public string PhoneNumber { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
}
