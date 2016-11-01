using Assignment3.Services.Entities;
using System.Collections.Generic;

namespace Assignment3.Services.DataAccess
{
    public interface IVideoDataMapper
    {
        List<Video> getAllVideos();
        List<Video> getAllVideosInChannel(int channelID);
        Channel getChannelById(int id);
        Video getVideoById(int id);
        int addVideo(Video vid);
        void deleteVideoById(int id);
    }
}