using System.Collections.Generic;
using Assignment3.Models;
using Assignment3.Services.DataAccess;
using Assignment3.Services.Entities;
using Assignment3.Services.Exceptions;
using System;
namespace Assignment3.Services
{
    public class VideoService : IVideoService
    {
        private readonly ITokenService _tokenService;
        private readonly IVideoDataMapper _videoMapper;
        private readonly IAccountDataMapper _accountMapper;
        public VideoService(ITokenService tokenService, IVideoDataMapper videoMapper, IAccountDataMapper accountMapper){
            _tokenService = tokenService;
            _videoMapper = videoMapper;
            _accountMapper = accountMapper;
        }

        private List<VideoDTO> videoToDTO(List<Video> videos) {
            List<VideoDTO> returnList = new List<VideoDTO>();
            foreach (Video vid in videos) {
                VideoDTO dto = new VideoDTO {
                    id = vid.id,
                    title = vid.title,
                    source = vid.source,
                    creator = vid.creator,
                    channelId = vid.channelId
                };
                returnList.Add(dto);
            }
            return returnList;
        }

        // Checks if the user exists and is validated correctly, returns the user object if he is.
        private User getValidatedUser(string accessToken) {
            string username = _tokenService.getUsernameFromTokenString(accessToken);
            if (username == null || username == "") {
                throw new InvalidParametersException("username not defined");
            }
            User loggedInUser = _accountMapper.findUserByUsername(username);
            if (loggedInUser == null) {
                throw new AppObjectNotFoundException("no user found with that username");
            }
            if(!_tokenService.validateUserToken(accessToken, loggedInUser.id)){
                throw new AppValidationException("You need to be logged in to use the video services");
            }
            return loggedInUser;
        }

        public List<VideoDTO> getAllVideos(string accessToken) {
            User loggedInUser = getValidatedUser(accessToken);
            List<Video> videos = _videoMapper.getAllVideos();
            return videoToDTO(videos);
        }

        public ChannelVideosDTO getAllVideosByChannel(string accessToken, int channelID) {
            User loggedInUser = getValidatedUser(accessToken);
            Channel channel = _videoMapper.getChannelById(channelID);
            if (channel == null) {
                throw new AppObjectNotFoundException("no channel found with this id");
            }
            List<Video> videos = _videoMapper.getAllVideosInChannel(channelID);
            ChannelVideosDTO channelVids = new ChannelVideosDTO {
                channelID = channel.ID,
                title = channel.title,
                videos = videoToDTO(videos)
            };
            return channelVids;
        }

        public Video getVideoByID(int videoID) {
            return new Video();
        }

        public VideoDTO addChannelVideo(string accessToken, int channelID, VideoDTO video) {

            User loggedInUser = getValidatedUser(accessToken);
            if (video.title == null || video.title == "" ||
                video.source == null || video.source == "") {
                    throw new InvalidParametersException("video title or source not defined");
            }

            if (_videoMapper.getChannelById(channelID) == null) {
                throw new AppObjectNotFoundException("no channel found with this id");
            }

            Video newVid = new Video();
            newVid.channelId = channelID;
            newVid.creator = loggedInUser.id;
            newVid.source = video.source;
            newVid.title = video.title;
            
            int vidId = _videoMapper.addVideo(newVid);

            video.channelId = channelID;
            video.creator = loggedInUser.id;
            video.id = vidId;

            return video;
        }

        public void deleteVideo(string accessToken, int videoID) {
            User loggedInUser = getValidatedUser(accessToken);
            Video vid = _videoMapper.getVideoById(videoID);
            if (vid == null) {
                throw new AppObjectNotFoundException("No video was found with that id");
            }
            if (vid.creator != loggedInUser.id) {
                throw new AppValidationException("Only the video creator can delete a video");
            }
            _videoMapper.deleteVideoById(videoID);
            return;
        }
    }
}

