
namespace Assignment3.Services
{
    public interface ITokenService {
        bool validateUserToken(string token, int userID);
        string createUserToken(int userID, string username);
    }
}