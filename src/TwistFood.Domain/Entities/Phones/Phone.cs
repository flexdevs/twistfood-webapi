using OnlineMarket.Domain.Common;
using TwistFood.Domain.Common;
using TwistFood.Domain.Entities.Users;

namespace TwistFood.Domain.Entities.Phones;

public class Phone : Auditable
{
    public long UserId { get; set; }
    public virtual User User { get; set; } = default!;
    public string PhoneId { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
