using OnlineMarket.Domain.Common;
using System.Drawing;

namespace TwistFood.Api.Models;

public class Location : BaseEntity
{
    public long UserId { get; set; }
    public virtual User User { get; set; } 
    public string ILocation { get; set; } = string.Empty;   
    public string AdditionInfo { get; set; }   = string.Empty;  
}
