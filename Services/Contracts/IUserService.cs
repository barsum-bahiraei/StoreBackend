using StoreBackend.Models;

namespace StoreBackend.Services.Contracts;
public interface IUserService
{
    Task<UserDetailViewModel> Detail(CancellationToken cancellation);
    Task<RegisterViewModel> Create(RegisterParameters user, CancellationToken cancellation);
    string GenerateJwtToken(string userName);
    Task<bool> IsValidUser(LoginParameters parameters);
}