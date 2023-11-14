using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskManagementApi.Models;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Account> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<Account> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                Account account = new Account()
                {
                    Email = registerModel.EmailAddress,
                    UserName = registerModel.UserName
                };
                IdentityResult identityResult = await _userManager.CreateAsync(
                    account,
                    registerModel.Password
                );
                if (identityResult.Succeeded)
                {
                    return CreatedAtAction(nameof(Register), "User Created Successfully");
                }
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                Account? account = await _userManager.FindByNameAsync(loginModel.Username);
                if (account != null)
                {
                    bool passwordValid = await _userManager.CheckPasswordAsync(
                        account,
                        loginModel.Password
                    );
                    if (passwordValid)
                    {
                        byte[] key = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!);
                        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
                        {
                            Subject = new ClaimsIdentity(
                                new []
                                {
                                    new Claim(ClaimTypes.Name, account.UserName!),
                                    new Claim(ClaimTypes.Email, account.Email!),
                                }
                            ),
                            Expires = DateTime.Now.AddHours(7),
                            Issuer = _configuration["JWT:ValidIssuer"],
                            Audience = _configuration["JWT:ValidAudience"],
                            SigningCredentials = new SigningCredentials(
                                new SymmetricSecurityKey(key),
                                SecurityAlgorithms.HmacSha512Signature
                            )
                        };
                        SecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                        return Ok(tokenHandler.WriteToken(token));
                    }

                    return BadRequest("Wrong Password");
                }

                return BadRequest("Wrong Username");
            }

            return BadRequest();
        }
    }
}
