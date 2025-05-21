using StoreBackend.Data;
using StoreBackend.Entities;
using StoreBackend.Helpers;
using StoreBackend.Models;
using StoreBackend.Services.Contracts;

namespace StoreBackend.Services.Implementation;

public class UserService(DatabaseContext context, IConfiguration configuration) : IUserService
{
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
