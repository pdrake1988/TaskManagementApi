using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Models;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;

        public AccountController(UserManager<Account> userManager, SignInManager<Account> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register([FromBody] AccountModel accountModel)
        {
            if (ModelState.IsValid)
            {
                Account account = new Account()
                {
                    UserName = accountModel.UserName,
                    Email = accountModel.EmailAddress
                };
                Task<IdentityResult> result = _userManager.CreateAsync(account, accountModel.Password);
                if (result.IsCompletedSuccessfully)
                {
                    return CreatedAtAction(nameof(Register),
                        _signInManager.PasswordSignInAsync(accountModel.UserName, accountModel.Password, true, false));
                }
            }
            return BadRequest();
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] AccountModel accountModel)
        {
            if (ModelState.IsValid)
            {
                return CreatedAtAction(nameof(Login),_signInManager.PasswordSignInAsync(accountModel.UserName, accountModel.Password,
                    true, false));
            }
            return NotFound();
        }

        [Authorize]
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            return CreatedAtAction(nameof(Logout), _signInManager.SignOutAsync());
        }
    }
}
