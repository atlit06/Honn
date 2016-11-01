using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Assignment3.Services;
using Assignment3.UnitTests.MockDataMappers;
using Assignment3.Services.Entities;

namespace Assignment3.UnitTests
{
    public class TokenServiceTest
    {
        [Fact]
        public void validateUserTokenTest() {
            List<Token> tokens = new List<Token>();
            System.TimeSpan duration1 = new System.TimeSpan(1, 0, 0, 0);
            string expires1 = (DateTime.Now.Add(duration1)).ToString();
            Token t1 = new Token {
                userID = 1,
                token = "test",
                expires = expires1
            };
            tokens.Add(t1);
            MockTokenDataMapper mapper = new MockTokenDataMapper(tokens);
            TokenService service = new TokenService(mapper);
            Assert.Equal(service.validateUserToken("Bearer test", 2), false);
            Assert.Equal(service.validateUserToken(null, 1), false);
            Assert.Equal(service.validateUserToken("incorrect", 1), false);
            Assert.Equal(service.validateUserToken("Bearer test", 1), true);
            System.TimeSpan duration2 = new System.TimeSpan(-1, 0, 0, 0);
            string expires2 = (DateTime.Now.Add(duration2)).ToString();
            Token t2 = new Token {
                userID = 2,
                token = "test2",
                expires = expires2
            };
            tokens.Add(t2);
            // Testing an expired token
            Assert.Equal(service.validateUserToken("test2", 2), false);
        }

        [Fact]
        public void createUserTokenTest() {
            List<Token> tokens = new List<Token>();
            MockTokenDataMapper mapper = new MockTokenDataMapper(tokens);
            TokenService service = new TokenService(mapper);
            service.createUserToken(1, "test");
            Assert.Equal(tokens.Count, 1);
            service.createUserToken(1, "test");
            Assert.Equal(tokens.Count, 1);
            Assert.Equal(service.validateUserToken("Bearer " + tokens.First().token, 1), true);
        }
    }
}