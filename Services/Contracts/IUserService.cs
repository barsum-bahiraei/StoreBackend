using StoreBackend.Models;

namespace StoreBackend.Services.Contracts;
public interface IUserService
{
    Task<UserDetailViewModel> Detail(CancellationToken cancellation);
    Task<UserCreateViewModel> Create(UserCreateDto user, CancellationToken cancellation);
}