using StoreBackend.Models;

namespace StoreBackend.Services.Contracts;
public interface IUserService
{
    Task<UserCreateViewModel> Create(UserCreateDTO user);
}