using System.Collections.Generic;
using Assignment3.Models;
using Assignment3.Services.DataAccess;
using Assignment3.Services.Exceptions;
using System;

namespace Assignment3.Services
{
    public class UserService : IUserService
    {
        private IUserDataMapper _mapper;
        private IAccountService _accService;

        public UserService(IAccountService accountService, IUserDataMapper mapper){
            _accService = accountService;
            _mapper = mapper;
        }

        public void addFavouriteVideo(AuthorizedUserDTO user, VideoDTO video){
            if(!_accService.verifyUser(user)){
                throw new InvalidParametersException("To favourite a video you must use a valid token and a valid username");
            }
            if(video.id == null){
                throw new InvalidParametersException("To favourite a video you must provide a proper Video ID");
            }
            
            try
            {
                _mapper.addFavourite(user, video);
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong with favouriting a video");
            }
        }

        public void addFriend(AuthorizedUserDTO user, PublicUserDTO friend){
            if(!_accService.verifyUser(user)){
                throw new InvalidParametersException("To add a friend you must use a valid token and a valid username");
            }
            if(friend.username == null){
                throw new InvalidParametersException("To add a friend you must provide a proper username for your friend");
            }
            
            try
            {
                _mapper.addFriend(user, friend);
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong with adding a friend");
            }
        }

        public void updateUserName(AuthorizedUserDTO user, UpdateUsernameDTO newUser){
            
        }
        public List<VideoDTO> getFavouriteVideos(){
            return new List<VideoDTO>();
        }
        public List<PublicUserDTO> getFriends(){
            return new List<PublicUserDTO>();
        }
    }
}
