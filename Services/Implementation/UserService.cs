using StoreBackend.Data;
using StoreBackend.Entities;
using StoreBackend.Helpers;
using StoreBackend.Models;
using StoreBackend.Services.Contracts;
using System.Security.Claims;

namespace StoreBackend.Services.Implementation;

public class UserService(DatabaseContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : IUserService
{
    public async Task<UserDetailViewModel> Detail()
    {
        var email = httpContextAccessor.HttpContext?.User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;

        var user = context.Users.FirstOrDefault(u => u.Email == email);
        return new UserDetailViewModel
        {
            Id = user.Id,
            Email = email,
            Family = user.Family,
            Name = user.Name,
        };
    }
    public async Task<UserCreateViewModel> Create(UserCreateDto user)
    {
        if (context.Users.Any(u => u.Email == user.Email))
        {
            throw new InvalidOperationException("Email is already in use.");
        }

        var newUser = new User
        {
            Name = user.Name,
            Family = user.Family,
            Email = user.Email,
            Password = HashHelper.HashSHA256(user.Password, configuration),
        };

        await context.Users.AddAsync(newUser);
        await context.SaveChangesAsync();

        return
            new UserCreateViewModel
            {
                Id = newUser.Id,
                Name = newUser.Name,
                Family = newUser.Family,
                Email = newUser.Email,
                Password = newUser.Password,
            };
    }
}
