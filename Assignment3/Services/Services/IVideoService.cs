using System.Collections.Generic;
using Assignment3.Models;
using Assignment3.Services.Entities;
namespace Assignment3.Services
{
    public interface IVideoService
    {
        List<VideoDTO> getAllVideos(string accessToken);
        ChannelVideosDTO getAllVideosByChannel(string accessToken, int channelID);
        Video getVideoByID(int videoID);
        VideoDTO addChannelVideo(string accessToken, int channelID, VideoDTO video);
        void deleteVideo(string accessToken, int videoID);
    }
}