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
        public IActionResult signUp([FromBody] UserDTO user)
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

        [HttpPost]
        [Route("updatePassword")]
        public IActionResult updatePassword([FromBody] UpdatePasswordDTO user) {
            string accessToken = Request.Headers["Authorization"];
            Console.WriteLine(accessToken);
            Console.WriteLine(user.username);
            Console.WriteLine(user.newPassword);
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
    }
}