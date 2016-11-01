using Assignment3.Services.DataAccess;
using Assignment3.Models;
using Assignment3.Services.Entities;
using Assignment3.Services.Exceptions;
using System;
using System.Text;
using System.IO;

namespace Assignment3.Services
{
    public class AccountService : IAccountService {

        private IAccountDataMapper _accountMapper;
        private ITokenService _tokenService;

        public AccountService(IAccountDataMapper accountMapper, ITokenService tokenService) {
            _accountMapper = accountMapper;
            _tokenService = tokenService;
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
                }
            if (_accountMapper.findUserByUsername(userDTO.username) != null) {
               throw new DuplicateException("Username is taken"); 
            }
            User newUser = new User {
                username = userDTO.username,
                email = userDTO.email,
                fullName = userDTO.fullName,
                password = userDTO.password
            };
            _accountMapper.createUser(newUser);
            return;
        }

        public AuthorizedUserDTO authenticateUser(UserDTO user)
        {
            if (user.username == null || user.username == "" ||
                user.password == null || user.password == "") {
                    throw new InvalidParametersException(
                        "To login a user the following parameters need to be defined: \n" +
                         "username, password"
                        );
                }
            User authenticateUser = _accountMapper.findUserByUsername(user.username);
            if (authenticateUser == null) {
                throw new AppObjectNotFoundException("No user is registered with that username");
            }
            if (user.password != authenticateUser.password) {
                throw new AppValidationException("Username and password don't match");
            }
            string tokenCode = _tokenService.createUserToken(authenticateUser.id, authenticateUser.username);
            return new AuthorizedUserDTO {
                accessToken = tokenCode,
                fullName = authenticateUser.fullName,
                username = authenticateUser.username
            };
        }

        public void updatePassword(UpdatePasswordDTO updatePass)
        {
          
            if (updatePass.username == null || updatePass.username == "") {
                throw new InvalidParametersException("username needs to be defined");
            }
            User currentUser = _accountMapper.findUserByUsername(updatePass.username);
            if (currentUser == null) {
                throw new AppObjectNotFoundException("No user found with this username");
            }
            if (!_tokenService.validateUserToken(updatePass.accessToken, currentUser.id)) {
                throw new AppValidationException("Your session has expired please log back in");
            }
            if (updatePass.newPassword == null || updatePass.newPassword == "") {
                throw new InvalidParametersException("newPassword parameter can not be empty");
            }
            _accountMapper.updateUserPassword(updatePass.username, updatePass.newPassword);
            return;
        }

        public void deleteUser(AuthorizedUserDTO user)
        {
            if (user.username == null || user.username == "" ||
            user.password == null || user.password == "") {
                throw new InvalidParametersException("To delete this user the user needs to provide his username and password.");
            }
            User currentUser = _accountMapper.findUserByUsername(user.username);
            if (currentUser == null) {
                throw new AppObjectNotFoundException("User not found");
            }
            if (user.password != currentUser.password || !_tokenService.validateUserToken(user.accessToken, currentUser.id)) {
                throw new AppValidationException();
            }
            _accountMapper.deleteUser(currentUser.id);
            return;
        }
    }
}