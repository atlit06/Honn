using System;
using System.Linq;
using Assignment3.Services.Entities;
using Assignment3.Services.Exceptions;
using Assignment3.Models;
using System.Collections.Generic;

namespace Assignment3.Services.DataAccess
{
    public class UserDataMapper : IUserDataMapper {
        private readonly AppDataContext _db;
        
        public UserDataMapper(AppDataContext db) {
            _db = db;
        }

        public int? getUserId(string username){
            return (from u in _db.Users
                    where u.username == username
                    select u.id).FirstOrDefault();
            
        }

        public void changeUsername(int userId, string newUsername){

            var user = (from u in _db.Users
                        where u.id == userId
                        select u).FirstOrDefault();
            user.username = newUsername;
            _db.SaveChanges();

        }

        public void addFavourite(int userId, int videoId){
            _db.Favourites.Add(new FavouriteVideo {videoId = videoId, userId = userId});
            _db.SaveChanges();
            return;
        }

        public void addFriend(int user, int friend){
            _db.Friends.Add(new Friend{friendee = user, friended = friend});  
            
            _db.SaveChanges();
            return;  
        }

        public List<VideoDTO> getFavouriteVideos(int userId)  { 
            
            List<VideoDTO> vids = (from fv in _db.Favourites
                    where fv.userId == userId
                    join v in _db.Videos on fv.videoId equals v.id
                    select new VideoDTO {
                        id = v.id,
                        title = v.title,
                        source = v.source,
                        creator = v.creator,
                        channelId = v.channelId
                    }).ToList();
            
            return vids;   
        }

        public List<PublicUserDTO> getFriends(int userId)     {
            return (from f in _db.Friends
                    where f.friendee == userId
                    join u in _db.Users on f.friended equals u.id
                    select new PublicUserDTO{
                        username = u.username,
                        email = u.email}).ToList();
        }

        public User getUserInfo(string username){
            return (from u in _db.Users
                    where u.username == username
                    select u).FirstOrDefault();
        }

    }
}