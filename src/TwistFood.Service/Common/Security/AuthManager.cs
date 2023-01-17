
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using TwistFood.Domain.Entities.Employees;
using TwistFood.Domain.Entities.Users;
using TwistFood.Service.Interfaces;

namespace TwistFood.Service.Security;
public class AuthManager : IAuthManager
{
    private readonly IConfiguration _config;

    public AuthManager(IConfiguration configuration)
    {
        _config = configuration.GetSection("Jwt");
    }

    public string GenerateOperatorToken(Operator @operator)
    {
        var claims = new[]
        {
            new Claim("Id", @operator.Id.ToString()),
            new Claim("FullName", @operator.FirstName + " " + @operator.LastName),
            new Claim("Email", @operator.Email),
            new Claim(ClaimTypes.Role, (@operator.IsHead == true) ? "head" : "nohead")
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecretKey"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new JwtSecurityToken(_config["Issuer"], _config["Audience"], claims,
            expires: DateTime.Now.AddDays(int.Parse(_config["Lifetime"])),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

    public string GenerateAdminToken(Admin admin)
    {
        var claims = new[]
        {
            new Claim("Id", admin.Id.ToString()),
            new Claim("FullName", admin.FirstName + " " + admin.LastName),
            new Claim(ClaimTypes.Email, admin.Email),
            new Claim(ClaimTypes.Role, (admin.IsHead == true) ? "head" : "nohead")
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecretKey"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new JwtSecurityToken(_config["Issuer"], _config["Audience"], claims,
            expires: DateTime.Now.AddDays(int.Parse(_config["Lifetime"])),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

    public string GenerateUserToken(User user)
    {
        var claims = new[]
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("FullName", user.FullName),
            new Claim("PhoneNumber",user.PhoneNumber)
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecretKey"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new JwtSecurityToken(_config["Issuer"], _config["Audience"], claims,
            expires: DateTime.Now.AddDays(int.Parse(_config["Lifetime"])),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

    }

}
