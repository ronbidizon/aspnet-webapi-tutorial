using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Models.AppUser> _userManager;
        private readonly Interfaces.ITokenService _tokenService;
        private readonly SignInManager<Models.AppUser> _signInManager;
        public AccountController(UserManager<Models.AppUser> userManager, Interfaces.ITokenService tokenService, SignInManager<Models.AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName ==  loginDto.Username.ToLower());
                if (user == null)
                    return Unauthorized("Invalid username.");

                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
                if (!result.Succeeded)
                    return Unauthorized("Invalid username or password."); 

                return Ok(new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                });
           
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] api.Dtos.Account.RegisterDto registerDto)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                var appUser = new api.Models.AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);
                
                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(new NewUserDto
                        {
                            UserName = appUser.UserName,
                            Email = appUser.Email,
                            Token = _tokenService.CreateToken(appUser)
                        });
                    }
                    else
                    {
                        return BadRequest(new { message = "User registered but failed to assign role." });
                    }
                }
                else
                {
                    return BadRequest(new { message = "User registration failed.", errors = createdUser.Errors });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}