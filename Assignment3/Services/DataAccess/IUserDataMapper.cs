using Assignment3.Services.Entities;
using Assignment3.Models;

namespace Assignment3.Services.DataAccess
{
    public interface IUserDataMapper
    {
         void addFavourite(UserDTO user, VideoDTO video);
         void addFriend(AuthorizedUserDTO user, PublicUserDTO friend);
    }
}