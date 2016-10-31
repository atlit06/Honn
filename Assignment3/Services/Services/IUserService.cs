using System;
using System.Collections.Generic;
using Assignment3.Services.DataAccess;
using Assignment3.Models;

namespace Assignment3.Services
{
    public interface IUserService
    {        
        void addFavouriteVideo(AuthorizedUserDTO user, VideoDTO video);
        void addFriend(AuthorizedUserDTO user, PublicUserDTO friend);
        void updateUserName(AuthorizedUserDTO user, UpdateUsernameDTO newUser);
        List<VideoDTO> getFavouriteVideos();
        List<PublicUserDTO> getFriends();

    }
}
