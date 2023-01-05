using OnlineMarket.Domain.Common;
using TwistFood.Domain.Common;

namespace TwistFood.Domain.Entities.Employees;

public class Admin : Employee
{
  
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
    public bool IsHead { get; set; } = false;
}
