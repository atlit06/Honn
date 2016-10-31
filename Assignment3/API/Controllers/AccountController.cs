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
                return new BadRequestObjectResult(e.Message);
            }
            catch (DuplicateException e) {
                return new BadRequestObjectResult(e.Message);
            }
            catch (Exception)
            {
                return new BadRequestObjectResult("Could not process the request");
            }
        }
    }
}
