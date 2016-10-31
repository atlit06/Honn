using System;
using System.Collections.Generic;
using Assignment3.Services.Entities;
using Assignment3.Services.DataAccess;

namespace Assignment3.Tests.MockDataMappers
{/*
    public class MockUserDataMapper : IUserDataMapper
    {
        public virtual ICollection<User>            Users           { get; set; }
        public virtual ICollection<Channel>         Channels        { get; set; }
        public virtual ICollection<Token>           Tokens          { get; set; }
        public virtual ICollection<Friend>          Friends         { get; set; }
        public virtual ICollection<ChannelUser>     ChannelUsers    { get; set; }
        public virtual ICollection<Video>           Videos          { get; set; } 
        public virtual ICollection<FavouriteVideo>  Favourites      { get; set; }

        public int? getUserId(string username){
            return (from u in Users
                    where u.username == username
                    select u.id).FirstOrDefault();
            
        }

        public void changeUsername(int userId, string newUsername){
            var user = (from u in Users
                        where u.id == userId
                        select u).FirstOrDefault();
            user.username = newUsername;
            SaveChanges();
        }

        public void addFavourite(int userId, int videoId){
            Favourites.Add(new FavouriteVideo {videoId = videoId, userId = userId});
            SaveChanges();
            return;
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

         public int id          { get; set; }
        public string title     { get; set; }
        public string source    { get; set; }
        public string creator   { get; set; }
        public int channelId    { get; set; }

        public List<PublicUserDTO> getFriends(int userId)     { 
            return (from f in Friends
                    where f.friendee == userId
                    join u in Users on f.friendee equals u.id
                    select new PublicUserDTO{   
                        username = u.username,
                        email = u.email}).ToList();
        }

    }*/
}