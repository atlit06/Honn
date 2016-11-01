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

        public void updateUsername(UpdateUsernameDTO newUser){
            // Make sure the object is not empty
            if( newUser.username == null || 
                newUser.password == null ||
                newUser.email    == null ||
                newUser.fullName == null ||
                newUser.accessToken == null ||
                newUser.newUsername == null){
                    throw new InvalidParametersException("All parameters of UpdateUsernameDTO    must be set");
                }
            // If the int is NULL  the user does not exist
            int? userId = _mapper.getUserId(newUser.username);
            if(userId == null){
                throw new InvalidParametersException("No user found");  
            }
            // Authenticate, if this equates to True we did not Authenticate with given information
            if(!_tokenService.validateUserToken(newUser.accessToken, (int)userId)){
                throw new InvalidParametersException("To favourite a video you must use a valid token and a valid username");
            }
            // Make a DataLayer Call
            _mapper.changeUsername((int)userId, newUser.newUsername);
        }

        public void addFavouriteVideo(NewFavouriteVideoDTO newFav){
            // Make sure the passed down object does not contain null variables
            if( newFav.username == null || 
                newFav.password == null ||
                newFav.email    == null ||
                newFav.fullName == null ||
                newFav.accessToken == null ||
                newFav.videoId == null){
                    throw new InvalidParametersException("All parameters of NewFavouriteVideoDTO must be set");
                }
            // if userId is null the user does not exist and we throw an exception 
            int? userId = _mapper.getUserId(newFav.username);
            if(userId == null){
                throw new InvalidParametersException("User does not exist in database");
            }

            // Make sure we are Authenticated using a authentication service 
            if(!_tokenService.validateUserToken(newFav.accessToken, (int)userId)){
                throw new InvalidParametersException("To favourite a video you must use a valid token and a valid username");
            }
            

            // Datalayer call to add favourite video to the favourites
            _mapper.addFavourite((int)userId, newFav.videoId);
            
        }

        public void addFriend(FriendDTO friendReq){
            // If userid is null the user does not exist
            int? userId = _mapper.getUserId(friendReq.username);
            if(userId == null){
                throw new InvalidParametersException("User does not exist in database");
            }

            // If friendId is null the friend does not exist
            int? friendId = _mapper.getUserId(friendReq.friendUsername);
            if(friendId == null){
                throw new InvalidParametersException("User does not exist in database");
            }

            // if friendUsername is null the friend does not exist
            if(friendReq.friendUsername == null){
                throw new InvalidParametersException("Friend username must be set in order to add a friend");
            }

            // Authenticate the user adding the friend
            if(!_tokenService.validateUserToken(friendReq.accessToken, (int)userId)){
                throw new InvalidParametersException("To add a friend you must use a valid token and a valid username");
            }
    
            // Datalayer call to create the 2 friends
            _mapper.addFriend((int)userId, (int)friendId);
        }
        
        public List<VideoDTO> getFavouriteVideos(PublicUserDTO user){
            // If the userId is nul the user does not exist
            int? userId = _mapper.getUserId(user.username);
            if(userId == null){
                throw new InvalidParametersException("User cannot be found");
            }
            // Do a Datalayer Call
            return _mapper.getFavouriteVideos((int)userId);
        }
        public List<PublicUserDTO> getFriends(PublicUserDTO user){
            // If the userId is nul the user does not exist
            int? userId = _mapper.getUserId(user.username);
            if(userId == null){
                throw new InvalidParametersException("User cannot be found");
            }

            // Do a DataLayer Call
            return _mapper.getFriends((int)userId);
        }
    }
}
