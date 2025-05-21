using StoreBackend.Models;

namespace StoreBackend.Services.Contracts;
public interface IUserService
{
    Task<UserDetailViewModel> Detail();
    Task<UserCreateViewModel> Create(UserCreateDto user);
}