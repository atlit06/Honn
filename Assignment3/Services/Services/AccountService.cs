using Assignment3.Services.DataAccess;
using Assignment3.Models;
using Assignment3.Services.Entities;

namespace Assignment3.Services
{
    public class AccountService : IAccountService {

        private  IAccountDataMapper _mapper;

        public AccountService(IAccountDataMapper mapper) {
            _mapper = mapper;
        }

        public void createUser(UserDTO userDTO)
        {
            if (
                userDTO.username == null || userDTO.username == "" ||
                userDTO.email == null || userDTO.email == "" ||
                userDTO.fullName == null || userDTO.fullName == "" ||
                userDTO.password == null || userDTO.password == ""
                )
                {
                    return;
                }
            return;
        }

        public AuthorizedUserDTO authenticateUser(UserDTO user)
        {
            return new AuthorizedUserDTO {
                accessToken = "Gegeg",
                fullName = "John Doe",
                username = "johnDoe",
                role = "admin"
            };
        }

        public void updatePassword(UpdatePasswordDTO updatePass)
        {
            return;
        }

        public void deleteUser(AuthorizedUserDTO user)
        {
            return;
        }

        public bool verifyUser(AuthorizedUserDTO user)
        {
            return true;
        }
    }
}