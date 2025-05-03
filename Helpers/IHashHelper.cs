namespace StoreBackend.Helpers;

public interface IHashHelper
{
    string HashSHA256(string str);
    string HashSHA512(string str);
}
