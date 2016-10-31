using Assignment3.Services.DataAccess;
using Assignment3.Models;

namespace Assignment3.Services
{
    public class AccountService : IAccountService {

        private  IAccountDataMapper _mapper;

        public AccountService(IAccountDataMapper mapper) {
            _mapper = mapper;
        }

        public void createUser(User user)
        {
            return;
        }

        public AuthorizedUserDTO authenticateUser(User user)
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