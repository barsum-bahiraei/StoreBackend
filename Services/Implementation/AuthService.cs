using Microsoft.IdentityModel.Tokens;
using StoreBackend.Data;
using StoreBackend.Helpers;
using StoreBackend.Models;
using StoreBackend.Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StoreBackend.Services.Implementation;

public class AuthService(IConfiguration configuration, DatabaseContext context) : IAuthService
{
    public string GenerateJwtToken(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new ArgumentException("Email cannot be null or empty", nameof(email));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Email, email),
            //new Claim(ClaimTypes.Role, "User")
        };

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(48),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool IsValidUser(LoginDTO loginModel)
    {
        var hashPassword = HashHelper.HashSHA256(loginModel.Password, configuration);
        var user = context.Users.FirstOrDefault(u => u.Email == loginModel.Email && u.Password == hashPassword);
        return user != null;
    }
}
