using System;
using System.Collections.Generic;
using Assignment3.Services.DataAccess;
using Assignment3.Models;

namespace Assignment3.Services
{
    public interface IUserService
    {        
        void addFavouriteVideo(AuthorizedUserDTO user, VideoDTO video);
        void addFriend(FriendDTO friendReq);
        void updateUserName(UpdateUsernameDTO newUser);
        List<VideoDTO> getFavouriteVideos(AuthorizedUserDTO user);
        List<PublicUserDTO> getFriends(AuthorizedUserDTO user);

    }
}
