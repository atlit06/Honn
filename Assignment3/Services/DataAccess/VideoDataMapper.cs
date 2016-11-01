using System;
using System.Linq;
using Assignment3.Services.Entities;
using System.Collections.Generic;

namespace Assignment3.Services.DataAccess
{
    public class VideoDataMapper : IVideoDataMapper {
        private readonly AppDataContext _db;
        public VideoDataMapper(AppDataContext db) {
            _db = db;
        }

        public List<Video> getAllVideos() {
            List<Video> vids = (from v in _db.Videos select v).ToList();
            return vids;
        }
        public List<Video> getAllVideosInChannel(int channelID) {
            List<Video> vids = (from v in _db.Videos where v.channelId == channelID select v).ToList();
            Console.WriteLine(vids);
            return vids;
        }
        public Channel getChannelById(int id) {
            return (from c in _db.Channels where c.ID == id select c).SingleOrDefault();
        }
        public int addVideo(Video vid) {
            _db.Videos.Add(vid);
            _db.SaveChanges();
            return vid.id;
        }
        public Video getVideoById(int id) {
            return (from v in _db.Videos where v.id == id select v).SingleOrDefault();
        }
        public void deleteVideoById(int id) {
            Video vid = (from v in _db.Videos where v.id == id select v).SingleOrDefault();
            if (vid == null) {
                return;
            }
            _db.Videos.Remove(vid);
            _db.SaveChanges();
        }
    }
}