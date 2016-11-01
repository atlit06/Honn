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
    public class UserServiceTest
    {
            
        


        [Fact]
        public void createUserSuccessTest() {
            MockTokenService tokenService = new MockTokenService();
            MockUserDataMapper mockMapper = new MockUserDataMapper();
            IUserService _service = new UserService(tokenService, new MockUserDataMapper());
        }
    }
}