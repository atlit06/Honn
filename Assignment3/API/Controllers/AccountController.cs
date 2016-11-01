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
    }
}
