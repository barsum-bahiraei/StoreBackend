using System.Security.Cryptography;
using System.Text;

namespace StoreBackend.Helpers;


public class PasswordHash
{
    private readonly string? _hasKey;

    public PasswordHash(IConfiguration configuration)
    {
        _hasKey = configuration.GetConnectionString("PasswordHashKey");

        if (string.IsNullOrEmpty(_hasKey))
        {
            throw new Exception("HashKey is not configured in appsettings.json!");
        }
    }

    public string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();

        var combinedPassword = password + _hasKey;
        var bytes = Encoding.UTF8.GetBytes(combinedPassword);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);

    }
}
