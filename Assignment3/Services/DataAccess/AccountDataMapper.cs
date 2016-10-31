using System;
using Assignment3.Services.Entities;

namespace Assignment3.Services.DataAccess
{
    public class AccountDataMapper : IAccountDataMapper {
        private readonly AppDataContext _db;
        public AccountDataMapper(AppDataContext db) {
            _db = db;
        }

        public void AddValue() {
            _db.Values.Add(new Value { test = 1 });
            _db.SaveChanges();
        }
    }
}