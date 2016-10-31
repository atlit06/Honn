using Assignment3.Services.Entities;

namespace Assignment3.Services.DataAccess
{
    public interface IAccountDataMapper
    {
        User findUserByUsername(string username);
        void createUser(User user);
    }
}