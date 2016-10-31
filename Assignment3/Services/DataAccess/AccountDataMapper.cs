using System;
using Assignment3.Services.Entities;

namespace Assignment3.Services.DataAccess
{
    public class AccountDataMapper : IAccountDataMapper {
        private readonly AppDataContext _db;
        public AccountDataMapper(AppDataContext db) {
            _db = db;
        }

        public User findUserByUsername(string username) {
            return new User();
        }

        public void createUser(User user) {
            return;
        }
    }
}