using OnlineMarket.Domain.Common;
using TwistFood.Domain.Common;

namespace TwistFood.Domain.Entities.Users;

public class User : Auditable
{
    public string? TelegramId { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
}
