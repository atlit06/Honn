using System;
using System.Linq;
using Assignment3.Services.Entities;
using Assignment3.Models;

namespace Assignment3.Services.DataAccess
{
    public class UserDataMapper : IUserDataMapper {
        private readonly AppDataContext _db;
        
        public UserDataMapper(AppDataContext db) {
            _db = db;
        }

        public void addFavourite(UserDTO user, VideoDTO video){
            _db.Favourites.Add(new FavouriteVideo {videoId = video.id, userId = user.username});
            _db.SaveChanges();
            return;
        }

        public void addFriend(AuthorizedUserDTO user, PublicUserDTO friend){
            _db.Friends.Add(new Friend{ friendee = user.id, friended = friend.id});
        }
    }
}