using Assignment3.Models;
using Assignment3.Services.Entities;
namespace Assignment3.Services
{
    public interface IAccountService {
       void createUser(UserDTO user);
       AuthorizedUserDTO authenticateUser(UserDTO user);
       void updatePassword(UpdatePasswordDTO updatePass);
       void deleteUser(AuthorizedUserDTO user);
       bool verifyUser(AuthorizedUserDTO user);
    }
}