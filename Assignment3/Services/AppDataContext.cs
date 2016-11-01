using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Assignment3.Services.Entities;

namespace Assignment3.Services
{
    public class AppDataContext : DbContext
    {
       public DbSet<User> Users                 { get; set; }
       public DbSet<Channel> Channels           { get; set; }
       public DbSet<Token> Tokens               { get; set; }
       public DbSet<Friend> Friends             { get; set; }
       public DbSet<ChannelUser> ChannelUsers   { get; set; }
       public DbSet<Video> Videos               { get; set; } 
       public DbSet<FavouriteVideo> Favourites  { get; set; }


        public AppDataContext(DbContextOptions<AppDataContext> options)
            : base(options)
        {
            this.Database.OpenConnection();
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}

