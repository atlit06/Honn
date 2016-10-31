
namespace Assignment3.Services
{
    public interface ITokenService {
        bool validateUserToken(string token, string username);
        string createUserToken(string username);
    }
}