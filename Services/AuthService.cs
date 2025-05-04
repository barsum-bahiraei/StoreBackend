using Microsoft.IdentityModel.Tokens;
using StoreBackend.Data;
using StoreBackend.Helpers;
using StoreBackend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StoreBackend.Services;

public class AuthService(IConfiguration configuration, DatabaseContext context, IHashHelper hashHelper)
{
    public string GenerateJwtToken(string userName)
    {
        if (string.IsNullOrEmpty(userName))
        {
            throw new ArgumentException("Username cannot be null or empty", nameof(userName));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, "User")
        };

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool IsValidUser(LoginModel loginModel)
    {
        var user = context.Users.FirstOrDefault(u => u.Username == loginModel.UserName);
        if (user == null)
        {
            return false;
        }
        var hashPassword = hashHelper.HashSHA256(loginModel.Password);
        if (hashPassword != user.Password)
        {
            return false;
        }
        return true;
    }
}
