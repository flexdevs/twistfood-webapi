using OnlineMarket.Domain.Common;

namespace TwistFood.Api.Models;

public class User : BaseEntity
{
    public string TelegramId { get; set; } = string.Empty;  
    public string PhoneNumber { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
}
