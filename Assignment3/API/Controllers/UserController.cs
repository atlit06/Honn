using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Assignment3.Models;
using Assignment3.Services;
using Assignment3.Services.Exceptions;

namespace Assignment3.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        // Our service class connection
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Add a new friend 
        /// example request body:
        /// {
        ///     "email": "johndoe@test.com",
        ///     "username": "johndoe",
        ///     "password": "john",
        ///     "fullName": "John Doe",
        ///     "accessToken": "xxxxxxxxxxx",
        ///     "friendUsername": "Doe John"
        /// }
        /// </summary>
        /// <param name="friendReq">the user object</param>
        /// <returns>201 created if the user is friended successfully</returns>
        [HttpPost]
        [Route("addFriend")]
        public IActionResult addFriend(FriendDTO friendReq)
        {
            // Basic API -> Service Cals
            // Receive Object Through Post/Get Request
            // Pass Object Too Service Layer
            // Catch Defined Errors Else Return 2xx Message
            try
            {
                _userService.addFriend(friendReq);
                return Ok(friendReq);
            }
            catch (InvalidParametersException e) {
                return BadRequest(e.Message);
            }
            catch (DuplicateException e) {
                return new BadRequestObjectResult(e.Message);
            }
            return BadRequest("h");
        }
        

        /// <summary>
        /// Add a favourite video
        /// example request body:
        /// {
        ///     "email": "johndoe@test.com",
        ///     "username": "johndoe",
        ///     "password": "john",
        ///     "fullName": "John Doe",
        ///     "accessToken": "xxxxxxxxxxx",
        ///     "videoId": "2"
        /// }
        /// </summary>
        /// <param name="newFavVid">the user object</param>
        /// <returns>201 created if video is favourited succesfully</returns>
        [HttpPost]
        [Route("addFavourite")]
        public IActionResult addFavourite(NewFavouriteVideoDTO newFavVid)
        {
            // Basic API -> Service Cals
            // Receive Object Through Post/Get Request
            // Pass Object Too Service Layer
            // Catch Defined Errors Else Return 2xx Message
            try
            {
                _userService.addFavouriteVideo(newFavVid);
  
                return Ok();
            }
            catch (InvalidParametersException e) {
                return BadRequest(e.Message);
            }
            catch (DuplicateException e) {
                return new BadRequestObjectResult(e.Message);
            }
        }



        /// <summary>
        /// Update a current users username
        /// example request body:
        /// {
        ///     "email": "johndoe@test.com",
        ///     "username": "johndoe",
        ///     "password": "john",
        ///     "fullName": "John Doe",
        ///     "accessToken": "xxxxxxxxxxx",
        ///     "newUsername": "John Doe 2.0"
        /// }
        /// </summary>
        /// <param name="user">the user object</param>
        /// <returns>201 created if username is updated succesfully</returns>
        [HttpPost]
        [Route("updateUsername")]
        public IActionResult updateUsername(UpdateUsernameDTO user)
        {
            // Basic API -> Service Cals
            // Receive Object Through Post/Get Request
            // Pass Object Too Service Layer
            // Catch Defined Errors Else Return 2xx Message
            try
            {
                _userService.updateUsername(user);
                return Ok();
            }
            catch (InvalidParametersException e) {
                return BadRequest(e.Message);
            }
            catch (DuplicateException e) {
                return new BadRequestObjectResult(e.Message);
            }
        }

        
        /// <summary>
        /// Get Favourite Videos
        /// example request body:
        /// {
        ///     "email": "johndoe@test.com",
        ///     "username": "johndoe",
        /// }
        /// </summary>
        /// <param name="user">the user object</param>
        /// <returns> 200 with a List of videos in a json format</returns>
        [HttpPost]
        [Route("getFavourites")]
        public IActionResult getFavourites(PublicUserDTO user)
        {
            // Basic API -> Service Cals
            // Receive Object Through Post/Get Request
            // Pass Object Too Service Layer
            // Catch Defined Errors Else Return 2xx Message
            try
            {
                return Ok(_userService.getFavouriteVideos(user));
            }
            catch (InvalidParametersException e) {
                return BadRequest(e.Message);
            }
            catch (DuplicateException e) {
                return new BadRequestObjectResult(e.Message);
            }
        }
        
        /// <summary>
        /// Get A List Of Friends
        /// example request body:
        /// {
        ///     "email": "johndoe@test.com",
        ///     "username": "johndoe"
        /// }
        /// </summary>
        /// <param name="user">the user object</param>
        /// <returns>200 and witha  list of friends in json format</returns>
        [HttpPost]
        [Route("getFriends")]
        public IActionResult getFriends(PublicUserDTO user)
        {
            // Basic API -> Service Cals
            // Receive Object Through Post/Get Request
            // Pass Object Too Service Layer
            // Catch Defined Errors Else Return 2xx Message
            try
            {
                return Ok(_userService.getFriends(user));
            }
            catch (InvalidParametersException e) {
                return BadRequest(e.Message);
            }
            catch (DuplicateException e) {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}
