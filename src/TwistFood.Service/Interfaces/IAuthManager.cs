
using TwistFood.Domain.Entities.Users;

namespace CarShop.Api.Interfaces;
public interface IAuthManager
{
    public string GenerateToken(User user);
}
