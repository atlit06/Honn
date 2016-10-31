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
        private ITokenService _tokenService;

        public UserService(ITokenService tokenService, IUserDataMapper mapper){
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public void updateUserName(UpdateUsernameDTO newUser){
            int? userId = _mapper.getUserId(newUser.username);
            if(userId == null){
                throw new InvalidParametersException("No user found");  
            }
            try
            {
                _mapper.changeUsername((int)userId, newUser.newUsername);
            }
            catch (Exception e)
            {
                throw new InvalidParametersException("Something went wrong with changing username");
            }
        }

        public void addFavouriteVideo(AuthorizedUserDTO user, VideoDTO video){
            if(!_tokenService.validateUserToken(user.username, user.accessToken)){
                throw new InvalidParametersException("To favourite a video you must use a valid token and a valid username");
            }
            
            try
            {
                int? userId = _mapper.getUserId(user.username);
                if(userId == null){
                    throw new InvalidParametersException("No Matching User");
                }
                _mapper.addFavourite((int)userId, video.id);
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong with favouriting a video");
            }
        }

        public void addFriend(FriendDTO friendReq){
            if(!_tokenService.validateUserToken(friendReq.username, friendReq.accessToken)){
                throw new InvalidParametersException("To add a friend you must use a valid token and a valid username");
            }
            
            try
            {
                int? userId = _mapper.getUserId(friendReq.username);
                int? friendId = _mapper.getUserId(friendReq.friendUsername);
                _mapper.addFriend((int)userId, (int)friendId);
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong with adding a friend");
            }
        }
        
        public List<VideoDTO> getFavouriteVideos(AuthorizedUserDTO user){
            int? userId = _mapper.getUserId(user.username);
            if(userId == null){
                throw new InvalidParametersException("User cannot be found");
            }
            return _mapper.getFavouriteVideos((int)userId);
        }
        public List<PublicUserDTO> getFriends(AuthorizedUserDTO user){
            int? userId = _mapper.getUserId(user.username);
            if(userId == null){
                throw new InvalidParametersException("User cannot be found");
            }
            return _mapper.getFriends((int)userId);
        }
    }
}
