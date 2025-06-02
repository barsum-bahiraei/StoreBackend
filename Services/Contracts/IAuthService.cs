using StoreBackend.Models;

namespace StoreBackend.Services.Contracts;
public interface IAuthService
{
    string GenerateJwtToken(string userName);
    Task<bool> IsValidUser(LoginDto loginModel);
}