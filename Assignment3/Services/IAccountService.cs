using Assignment3.Models;
namespace Assignment3.Services
{
    public interface IAccountService {
       void createUser(User user);
       AuthorizedUserDTO authenticateUser(User user);
       void updatePassword(UpdatePasswordDTO updatePass);
       void deleteUser(AuthorizedUserDTO user);
       bool verifyUser(AuthorizedUserDTO user);
    }
}