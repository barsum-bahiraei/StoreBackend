using StoreBackend.Models;

namespace StoreBackend.Services.Contracts;
public interface IAuthService
{
    string GenerateJwtToken(string userName);
    bool IsValidUser(LoginDto loginModel);
}