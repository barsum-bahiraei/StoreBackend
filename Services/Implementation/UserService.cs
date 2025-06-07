using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StoreBackend.Data;
using StoreBackend.Entities;
using StoreBackend.Helpers;
using StoreBackend.Models;
using StoreBackend.Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StoreBackend.Services.Implementation;

public class UserService(DatabaseContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : IUserService
{
    public async Task<UserDetailViewModel> Detail(CancellationToken cancellation)
    {
        //var aaa = new CancellationTokenSource();
        //aaa.CancelAfter(1000);
        var email = httpContextAccessor.HttpContext?.User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;

        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellation);
        return new UserDetailViewModel
        {
            Id = user.Id,
            Email = email,
            Family = user.Family,
            Name = user.Name,
        };
    }
    public async Task<RegisterViewModel> Create(RegisterParameters parameters, CancellationToken cancellation)
    {
        if (await context.Users.AnyAsync(u => u.Email == parameters.Email))
        {
            return null;
        }

        var newUser = new User
        {
            Name = parameters.Name,
            Family = parameters.Family,
            Email = parameters.Email,
            Password = HashHelper.HashSHA256(parameters.Password, configuration),
        };

        await context.Users.AddAsync(newUser);
        await context.SaveChangesAsync();
        return
            new RegisterViewModel
            {
                Id = newUser.Id,
                Name = newUser.Name,
                Family = newUser.Family,
                Email = newUser.Email,
                Token = GenerateJwtToken(newUser.Email),
            };
    }

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
            new Claim(ClaimTypes.Email, email),
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

    public async Task<bool> IsValidUser(LoginParameters parameters)
    {
        var hashPassword = HashHelper.HashSHA256(parameters.Password, configuration);
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == parameters.Email && u.Password == hashPassword);
        return user != null;
    }
}
