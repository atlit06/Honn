using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace RuTube_Web_Api.SQLite
{
    public class DBConnector : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./Database.db");
        }
    }
    
}
