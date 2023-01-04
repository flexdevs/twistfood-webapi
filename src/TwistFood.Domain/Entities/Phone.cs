using OnlineMarket.Domain.Common;

namespace TwistFood.Api.Models;

public class Phone : BaseEntity
{
    public long UserId { get; set; }    
    public virtual User User { get; set; } = default!;
    public string PhoneId { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
