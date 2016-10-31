using Assignment3.Services.Entities;

namespace Assignment3.Services.DataAccess
{
    public interface ITokenDataMapper
    {
        void createOrUpdateUserToken(Token token);
        Token getTokenByUsername(string username);
    }
}
