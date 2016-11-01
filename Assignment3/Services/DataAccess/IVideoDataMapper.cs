using Assignment3.Services.Entities;
using System.Collections.Generic;

namespace Assignment3.Services.DataAccess
{
    public interface IVideoDataMapper
    {
        List<Video> getAllVideos();
    }
}