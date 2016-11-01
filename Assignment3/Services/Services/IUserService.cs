using System;
using System.Collections.Generic;
using Assignment3.Services.DataAccess;
using Assignment3.Models;

namespace Assignment3.Services
{
    public interface IUserService
    {        
        void addFavouriteVideo(NewFavouriteVideoDTO newFav);
        void addFriend(FriendDTO friendReq);
        void updateUsername(UpdateUsernameDTO newUser);
        List<VideoDTO> getFavouriteVideos(PublicUserDTO user);
        List<PublicUserDTO> getFriends(PublicUserDTO user);
        ProfileDTO getProfile(PublicUserDTO user);

    }
}
