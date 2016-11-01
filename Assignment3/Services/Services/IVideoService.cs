using System.Collections.Generic;
using Assignment3.Models;
using Assignment3.Services.Entities;
namespace Assignment3.Services
{
    public interface IVideoService
    {
        List<VideoDTO> getAllVideos(AuthorizedUserDTO user);
        List<VideoDTO> getAllVideosByChannel(AuthorizedUserDTO user, int channelID);
        Video getVideoByID(int videoID);
        void addChannelVideo(AuthorizedUserDTO user, int channelID, VideoDTO video);
        void deleteVideo(AuthorizedUserDTO user, int videoID);
    }
}