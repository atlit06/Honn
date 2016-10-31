using System;
using System.Linq;
using Assignment3.Services.Entities;

namespace Assignment3.Services.DataAccess
{
    public class TokenDataMapper : ITokenDataMapper {
        private readonly AppDataContext _db;
        public TokenDataMapper(AppDataContext db) {
            _db = db;
        }

        public Token getTokenByUsername(string username) {
            return (from t in _db.Tokens
                    where username == t.userID
                    select t).FirstOrDefault();
        }

        public void createOrUpdateUserToken(Token token) {
            Token existingToken = (from t in _db.Tokens
                           where token.userID == t.userID
                           select t).FirstOrDefault();
            if (existingToken == null) {
                _db.Tokens.Add(token);
                _db.SaveChanges();
            } else {
                existingToken.token = token.token;
                existingToken.expires = token.expires;
                _db.SaveChanges();
            }
        }
    }
}