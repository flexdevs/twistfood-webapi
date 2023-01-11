
using TwistFood.Domain.Entities.Employees;
using TwistFood.Domain.Entities.Users;

namespace TwistFood.Service.Interfaces;
public interface IAuthManager
{
    public string GenerateUserToken(User user);
    public string GenerateOperatorToken(Operator @operator);
    public string GenerateAdminToken(Admin admin);
}
