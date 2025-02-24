using api.DTOs.Account;
using api.Interfaces;
using api.Mappers;
using api.models;
using api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,ITokenService tokenService,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
             _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try{
                if(!ModelState.IsValid) return BadRequest(ModelState);
                var appUser = new AppUser
                {
                    UserName = registerDto.Fullname,
                    Email = registerDto.Email,
                    PhoneNumber = registerDto.PhoneNumber,
                    UserType = registerDto.UserType,
                };

                var createdUser = await _userManager.CreateAsync(appUser,registerDto.Password);

                if(createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser,registerDto.UserType);
                    if(roleResult.Succeeded)
                    {
                        return Ok(appUser.toRegisterResponseDto(_tokenService.CreateToken(appUser)));
                    } 
                    else{
                    return StatusCode(500,roleResult.Errors);
                    };
                }
                else{
                    return StatusCode(500,createdUser.Errors);
                }


            }catch(Exception e)
            {
                return StatusCode(500,e.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email);
            if(user == null) return BadRequest("User not found");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password,false);

            if(!result.Succeeded) return BadRequest("Invalid Password");

            return Ok(user.toRegisterResponseDto(_tokenService.CreateToken(user)));
        }
    }
}