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
            return (from v in _db.Videos select v).ToList();
        }
    }
}