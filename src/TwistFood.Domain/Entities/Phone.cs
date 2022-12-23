namespace TwistFood.Api.Models;

public class Phone
{
    public long Id { get; set; }
    public long UserId { get; set; }    
    public virtual User User { get; set; }
    public string PhoneId { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
