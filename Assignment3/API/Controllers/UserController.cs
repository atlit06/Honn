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
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("addFriend")]
        public IActionResult addFriend([FromBody] FriendDTO frendReq)
        {
            try
            {
                _userService.addFriend(frendReq);
                return Ok();
            }
            catch (InvalidParametersException e) {
                return BadRequest(e.Message);
            }
            catch (DuplicateException e) {
                return new BadRequestObjectResult(e.Message);
            }
        }
        
        [HttpPost]
        [Route("addFavourite")]
        public IActionResult addFavourite([FromBody] FriendDTO frendReq)
        {
            try
            {
                _userService.addFriend(frendReq);
                return Ok();
            }
            catch (InvalidParametersException e) {
                return BadRequest(e.Message);
            }
            catch (DuplicateException e) {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [HttpPost]
        [Route("updateUsername")]
        public IActionResult updateUsername([FromBody] FriendDTO frendReq)
        {
            try
            {
                _userService.addFriend(frendReq);
                return Ok();
            }
            catch (InvalidParametersException e) {
                return BadRequest(e.Message);
            }
            catch (DuplicateException e) {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [HttpPost]
        [Route("getFavourites")]
        public IActionResult getFavourites([FromBody] FriendDTO frendReq)
        {
            try
            {
                _userService.addFriend(frendReq);
                return Ok();
            }
            catch (InvalidParametersException e) {
                return BadRequest(e.Message);
            }
            catch (DuplicateException e) {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [HttpPost]
        [Route("getFriends")]
        public IActionResult getFriends([FromBody] FriendDTO frendReq)
        {
            try
            {
                _userService.addFriend(frendReq);
                return Ok();
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
