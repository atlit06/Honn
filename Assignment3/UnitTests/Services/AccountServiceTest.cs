using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Assignment3.Services;
using Assignment3.UnitTests.MockDataMappers;
using Assignment3.Services.DataAccess;
using Assignment3.Services.Entities;
using Assignment3.Services.Exceptions;
using Assignment3.Models;

namespace Assignment3.UnitTests
{
    public class MockAccountDataMapper : IAccountDataMapper
    {
        private int findUserCallCount;
        private int createUserCallCount;
        private int updateUserCallCount;
        private int deleteUserCallCount;
        private bool _findUser;
        public MockAccountDataMapper(bool findUser) {
            findUserCallCount = 0;
            createUserCallCount = 0;
            updateUserCallCount = 0;
            deleteUserCallCount = 0;
            _findUser = findUser;
        }
        public User findUserByUsername(string username) {
            findUserCallCount += 1;
            if (_findUser) {
                return new User {
                    id = 1,
                    username = username,
                    password = "test",
                    email = "test",
                    fullName = "test"
            };
           } else {
               return null;
           }
        }
        public void createUser(User user) {
            createUserCallCount += 1;
        }
        public void updateUserPassword(string username, string password) {
            updateUserCallCount += 1;
        }
        public void deleteUser(int userID) {
            deleteUserCallCount += 1;
        }
        public int getFindCallCount() {
            return findUserCallCount;
        }
        public int getCreateCallCount() {
            return createUserCallCount;
        }
        public int getUpdateCallCount() {
            return updateUserCallCount;
        }
        public int getDeleteCallCount() {
            return deleteUserCallCount;
        }
    }
    public class MockTokenService : ITokenService
    {
        private int validateCallCount;
        private int createCallCount;
        public MockTokenService() {
            validateCallCount = 0;
            createCallCount = 0;
        }

        public bool validateUserToken(string token, int userID) {
            validateCallCount += 1;
            return true;
        }
        public string createUserToken(int userID, string username) {
            createCallCount += 1;
            return "test";
        }
    }
    public class AccountServiceTest
    {
        [Fact]
        public void createUserFailTest() {
            MockAccountDataMapper mapper = new MockAccountDataMapper(true);
            MockTokenService tokenService = new MockTokenService();
            AccountService service = new AccountService(mapper, tokenService);

            UserDTO user = new UserDTO {
                username = "test",
                email = "test",
                fullName = "test"
            };
            Exception ex = Assert.Throws<InvalidParametersException>( () => service.createUser(user));

            user.email = "";
            Exception ex2 = Assert.Throws<InvalidParametersException>( () => service.createUser(user));

            UserDTO user2 = new UserDTO {
                username = "test",
                email = "test",
                fullName = "test",
                password = "test"
            };
            Exception ex3 = Assert.Throws<DuplicateException>( () => service.createUser(user2));
        }

        [Fact]
        public void createUserSuccessTest() {
            MockAccountDataMapper mapper = new MockAccountDataMapper(false);
            MockTokenService tokenService = new MockTokenService();
            AccountService service = new AccountService(mapper, tokenService);

            UserDTO user = new UserDTO {
                username = "test",
                email = "test",
                fullName = "test",
                password = "test"
            };
            service.createUser(user);
            Assert.Equal(1, mapper.getCreateCallCount());

        }

        [Fact]
        public void authenticateUserTest() {
            MockAccountDataMapper mapper = new MockAccountDataMapper(false);
            MockTokenService tokenService = new MockTokenService();
            AccountService service = new AccountService(mapper, tokenService);

            UserDTO user = new UserDTO {
                username = "test"
            };
            Exception ex = Assert.Throws<InvalidParametersException>( () => service.authenticateUser(user));

            UserDTO user2 = new UserDTO {
                username = "test",
                password = "test"
            };
            Exception ex2 = Assert.Throws<AppObjectNotFoundException>( () => service.authenticateUser(user2));
            Assert.Equal(mapper.getFindCallCount(), 1);

            mapper = new MockAccountDataMapper(true);
            service = new AccountService(mapper, tokenService);

            UserDTO user3 = new UserDTO {
                username = "test",
                password = "test2"
            };
            Exception ex3 = Assert.Throws<AppValidationException>( () => service.authenticateUser(user3));

            UserDTO user4 = new UserDTO {
                username = "test",
                password = "test"
            };
            AuthorizedUserDTO signedIn = service.authenticateUser(user4);
            Assert.Equal(signedIn.username, "test");
            Assert.Equal(signedIn.accessToken, "test");
            Assert.Equal(signedIn.fullName, "test");

        }

        [Fact]
        public void updatePasswordTest() {
            MockAccountDataMapper mapper = new MockAccountDataMapper(false);
            MockTokenService tokenService = new MockTokenService();
            AccountService service = new AccountService(mapper, tokenService);
            UpdatePasswordDTO user = new UpdatePasswordDTO {
                newPassword = "test"
            };
            Exception ex = Assert.Throws<InvalidParametersException>( () => service.updatePassword(user));
            UpdatePasswordDTO user2 = new UpdatePasswordDTO {
                username = "test",
                newPassword = "test"
            };
            Exception ex2 = Assert.Throws<AppObjectNotFoundException>( () => service.updatePassword(user2));
            mapper = new MockAccountDataMapper(true);
            service = new AccountService(mapper, tokenService);
            UpdatePasswordDTO user3 = new UpdatePasswordDTO {
                username = "test",
            };
            Exception ex3 = Assert.Throws<InvalidParametersException>( () => service.updatePassword(user3));
            Assert.Equal(ex3.Message, "newPassword parameter can not be empty");
            UpdatePasswordDTO user4 = new UpdatePasswordDTO {
                username = "test",
                newPassword = "test"
            };
            service.updatePassword(user4);
            Assert.Equal(mapper.getUpdateCallCount(), 1);
        }

        [Fact]
        public void deleteUserTest() {
            MockAccountDataMapper mapper = new MockAccountDataMapper(false);
            MockTokenService tokenService = new MockTokenService();
            AccountService service = new AccountService(mapper, tokenService);

            AuthorizedUserDTO user = new AuthorizedUserDTO {
                username = "test"
            };
            Exception ex = Assert.Throws<InvalidParametersException>( () => service.deleteUser(user));

            AuthorizedUserDTO user2 = new AuthorizedUserDTO {
                username = "test",
                password = "test"
            };
            Exception ex2 = Assert.Throws<AppObjectNotFoundException>( () => service.deleteUser(user2));

            mapper = new MockAccountDataMapper(true);
            service = new AccountService(mapper, tokenService);

            AuthorizedUserDTO user3 = new AuthorizedUserDTO {
                username = "test",
                password = "test2"
            };
            Exception ex3 = Assert.Throws<AppValidationException>( () => service.deleteUser(user3));

            AuthorizedUserDTO user4 = new AuthorizedUserDTO {
                username = "test",
                password = "test"
            };
            service.deleteUser(user4);
            Assert.Equal(mapper.getDeleteCallCount(), 1);
        }
    }
}