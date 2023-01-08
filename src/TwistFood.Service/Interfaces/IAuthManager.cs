
using TwistFood.Domain.Entities.Employees;
using TwistFood.Domain.Entities.Users;

namespace CarShop.Api.Interfaces;
public interface IAuthManager
{
    public string GenerateUserToken(User user);
    public string GenerateOperatorToken(Operator @operator);
}
