using Assignment3.Services.DataAccess;
using Assignment3.Models;
using Assignment3.Services.Entities;
using Assignment3.Services.Exceptions;
using System;

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
                    throw new InvalidParametersException(
                        "To register a new user the following parameters need to be defined: \n" +
                         "username, email, fullName, password"
                        );
                    return;
                }
            if (_mapper.findUserByUsername(userDTO.username) != null) {
               throw new DuplicateException("Username is taken"); 
            }
            User newUser = new User {
                username = userDTO.username,
                email = userDTO.email,
                fullName = userDTO.fullName,
                password = userDTO.password
            };
            try
            {
                _mapper.createUser(newUser);
            } catch (Exception e) {
                throw new Exception("something wen wrong when creating the user");
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