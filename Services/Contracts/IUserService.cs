using StoreBackend.Models;

namespace StoreBackend.Services.Contracts;
public interface IUserService
{
    UserCreateViewModel Create(UserCreateDTO user);
}