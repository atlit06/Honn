using System;
using System.Collections.Generic;
using Assignment3.Services.Entities;
using Assignment3.Services.DataAccess;

namespace Assignment3.Tests.MockDataMappers
{
    public class MockTokenDataMapper : ITokenDataMapper
    {
        private List<Token> _tokens;
        public MockTokenDataMapper(List<Token> tokens) {
            _tokens = tokens;
        }
        public MockTokenDataMapper() {
            _tokens = new List<Token>();
        }

        public Token getTokenByUserID(int userID) {
            foreach (Token token in _tokens) {
                if (token.userID == userID) {
                    return token;
                }
            }
            return null;
        }

        public void createOrUpdateUserToken(Token token) {
            foreach (Token existingToken in _tokens) {
                if (existingToken.userID == token.userID) {
                    existingToken.token = token.token;
                    existingToken.expires = token.expires;
                    return;
                }
            }
            _tokens.Add(token);
        }
        public List<Token> getTokens() {
            return _tokens;
        }
    }
}