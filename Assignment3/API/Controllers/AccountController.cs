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
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        /// <summary>
        /// Sign up as a new user 
        /// example request body:
        /// {
        ///     "email": "johndoe@test.com",
        ///     "username": "johndoe",
        ///     "password": "john",
        ///     "fullName": "John Doe"
        /// }
        /// </summary>
        /// <param name="user">the user object</param>
        /// <returns>201 created if the user signed up successfully</returns>
        [HttpPost]
        [Route("signup")]
        public IActionResult signUp(UserDTO user)
        {
            try
            {
                _accountService.createUser(user);
                return new StatusCodeResult(201);
            }
            catch (InvalidParametersException e) {
                return BadRequest(e.Message);
            }
            catch (DuplicateException e) {
                return new BadRequestObjectResult(e.Message);
            }
        }
        
        /// <summary>
        /// Login as an existing user
        /// Example request body:
        /// {
        ///     "username": "johndoe",
        ///     "password": "john"
        /// }
        /// Example response body:
        /// {
        ///     "accessToken": "sampleToken",
        ///     "fullName": "John Doe",
        ///     "username": "johndoe"
        /// }
        /// </summary>
        /// <param name="user">the user object containing username and password</param>
        /// <returns>200 ok for a succesful login along with an access token</returns>
        [HttpPost]
        [Route("login")]
        public IActionResult login(UserDTO user)
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

        /// <summary>
        /// Update a password of a logged in user
        /// Example request body:
        /// {
        ///     "username": "johndoe",
        ///     "newPassword": "doe"
        /// }
        /// </summary>
        /// <param name="user">the user object containing username and password</param>
        /// <returns>200 ok for a succesful update</returns>
        [HttpPut]
        [Route("updatePassword")]
        public IActionResult updatePassword(UpdatePasswordDTO user) {
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

        /// <summary>
        /// delete a logged in user
        /// Example request body:
        /// {
        ///     "username": "johndoe",
        ///     "Password": "doe"
        /// }
        /// </summary>
        /// <param name="user">the user object containing username and password</param>
        /// <returns>200 ok for a succesful deletion</returns>
        [HttpPost]
        [Route("deleteUser")]
        public IActionResult deleteUser(AuthorizedUserDTO user) {
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
    }
}
