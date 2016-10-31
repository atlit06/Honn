using Assignment3.Services.Entities;
using Assignment3.Models;
using System.Collections.Generic;

namespace Assignment3.Services.DataAccess
{
    public interface IUserDataMapper
    {
        int? getUserId(string username);
        void changeUsername(int userId, string newUsername);
        void addFavourite(int userId, int videoId);
        void addFriend(int user, int friend);
        List<VideoDTO> getFavouriteVideos(int userId);
        List<PublicUserDTO> getFriends(int userId);
    }
}