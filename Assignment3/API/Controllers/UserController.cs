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
        [Route("friend")]
        public IActionResult signUp([FromBody] FriendDTO frendReq)
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
        /*
        [HttpPost]
        [Route("login")]
        public IActionResult login([FromBody] UserDTO user)
        {
            try
            {
                AuthorizedUserDTO authenticatedUser = _accountService.authenticateUser(user);
                return Ok(new {
                    accessToken = authenticatedUser.accessToken,
                    username = authenticatedUser.username,
                    fullName = authenticatedUser.fullName
                });
            }
            catch (InvalidParametersException e) {
                return BadRequest(e.Message);
            }
            catch (AppObjectNotFoundException e) {
                return NotFound(e.Message);
            }
            catch (AppValidationException) {
                return Unauthorized();
            }
        }

        [HttpPut]
        [Route("updatePassword")]
        public IActionResult updatePassword([FromBody] UpdatePasswordDTO user) {
            string accessToken = Request.Headers["Authorization"];
            user.accessToken = accessToken;
            try
            {
                _accountService.updatePassword(user);
                return Ok();
            }
            catch (InvalidParametersException e) {
                return BadRequest(e.Message);
            }
            catch (AppObjectNotFoundException e) {
                return NotFound(e.Message);
            }
            catch (AppValidationException) {
                return Unauthorized();
            }
        }

        [HttpDelete]
        [Route("deleteUser")]
        public IActionResult deleteUser([FromBody] AuthorizedUserDTO user) {
            string accessToken = Request.Headers["Authorization"];
            user.accessToken = accessToken;
            try
            {
                _accountService.deleteUser(user);
                return Ok();
            }
            catch (InvalidParametersException e) {
                return BadRequest(e.Message);
            }
            catch (AppObjectNotFoundException e) {
                return NotFound(e.Message);
            }
            catch (AppValidationException) {
                return Unauthorized();
            }

        }
        */
    }
}
