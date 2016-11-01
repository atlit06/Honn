using System.Collections.Generic;
using Assignment3.Models;
using Assignment3.Services.DataAccess;
using Assignment3.Services.Entities;
using Assignment3.Services.Exceptions;
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

        public List<VideoDTO> getAllVideos(AuthorizedUserDTO user) {
            if (user.username == null || user.username == "") {
                throw new InvalidParametersException("username not defined");
            }
            User loggedInUser = _accountMapper.findUserByUsername(user.username);
            if (loggedInUser == null) {
                throw new AppObjectNotFoundException("no user found with that username");
            }
            if(!_tokenService.validateUserToken(user.accessToken, loggedInUser.id)){
                throw new AppValidationException("You need to be logged in to use the video services");
            }
            List<Video> videos = _videoMapper.getAllVideos();
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
        public List<VideoDTO> getAllVideosByChannel(AuthorizedUserDTO user, int channelID) {
            return new List<VideoDTO>();
        }
        public Video getVideoByID(int videoID) {
            return new Video();
        }
        public void addChannelVideo(AuthorizedUserDTO user, int channelID, VideoDTO video) {
            return;
        }
        public void deleteVideo(AuthorizedUserDTO user, int videoID) {
            return;
        }
    }
}

