using System;
using System.Linq;
using System.Collections.Generic;
using Assignment3.Services.Entities;
using Assignment3.Services.DataAccess;
using Assignment3.Models;

namespace Assignment3.UnitTests.MockDataMappers
{
    public class MockUserDataMapper : IUserDataMapper
    {
        public List<User>            Users           { get; set; }
        public List<Channel>         Channels        { get; set; }
        public List<Token>           Tokens          { get; set; }
        public List<Friend>          Friends         { get; set; }
        public List<ChannelUser>     ChannelUsers    { get; set; }
        public List<Video>           Videos          { get; set; } 
        public List<FavouriteVideo>  Favourites      { get; set; }
        
        
        public int? getUserId(string username){
            return (from u in Users
                    where u.username == username
                    select u.id).FirstOrDefault();
            
        }

        public MockUserDataMapper(){
            
            Users = new List<User>();
            Channels = new List<Channel>();
            Tokens = new List<Token>();
            Friends = new List<Friend>();
            ChannelUsers = new List<ChannelUser>();
            Videos = new List<Video>();
            Favourites = new List<FavouriteVideo>();
        }

        public void changeUsername(int userId, string newUsername){
            var user = (from u in Users
                        where u.id == userId
                        select u).FirstOrDefault();
            user.username = newUsername;
        }
        public void addFavourite(int userId, int videoId){
            Favourites.Add(new FavouriteVideo {videoId = videoId, userId = userId});
        }
        public void addFriend(int user, int friend){
            Friends.Add(new Friend{friendee = user, friended = friend});    
        }
        public List<VideoDTO> getFavouriteVideos(int userId)  { 
            return (from fv in Favourites
                    where fv.userId == userId
                    join v in Videos on fv.videoId equals v.id
                    select new VideoDTO {
                        id = v.id,
                        title = v.title,
                        source = v.source,
                        creator = v.creator,
                        channelId = v.channelId
                    }).ToList();
        }
        
        public List<PublicUserDTO> getFriends(int userId)     { 
            return (from f in Friends
                    where f.friendee == userId
                    join u in Users on f.friendee equals u.id
                    select new PublicUserDTO{   
                        username = u.username,
                        email = u.email}).ToList();
        }

        public User getUserInfo(string username){
            return (from u in Users
                    where u.username == username
                    select u).FirstOrDefault();
        }
    }
}