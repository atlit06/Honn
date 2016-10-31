using System;
using System.Linq;
using Assignment3.Services.Entities;

namespace Assignment3.Services.DataAccess
{
    public class AccountDataMapper : IAccountDataMapper {
        private readonly AppDataContext _db;
        public AccountDataMapper(AppDataContext db) {
            _db = db;
        }

        public User findUserByUsername(string username) {
            return (from u in _db.Users
                    where username == u.username
                    select u).SingleOrDefault();
        }

        public void createUser(User user) {
            _db.Users.Add(user);
            _db.SaveChanges();
            return;
        }

        public void updateUserPassword(string username, string password) {
            User usr = (from u in _db.Users
                        where username == u.username
                        select u).SingleOrDefault();
            usr.password = password;
            _db.SaveChanges();
        }
        public void deleteUser(string username) {
            /*var userChannels = (from c in _db.ChannelUsers
                                where username == c.userID
                                select c);
                                */
            User usr = (from u in _db.Users
                        where username == u.username
                        select u).SingleOrDefault();
            _db.Remove(usr);
            Token userToken = (from t in _db.Tokens
                               where username == t.userID
                               select t).SingleOrDefault();
            _db.Remove(userToken);
            _db.SaveChanges();
        }
    }
}