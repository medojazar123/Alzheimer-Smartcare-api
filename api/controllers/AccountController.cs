using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using api.DTOs.Account;
using api.Interfaces;
using api.Mappers;
using api.models;
using api.Services;
using Microsoft.AspNetCore.Authorization;
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

                var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
                if (existingUser != null)
                {
                    return BadRequest(new { message = "Email is already registered" });
                }
                var appUser = new AppUser
                {
                    UserName = registerDto.Email,
                    Email = registerDto.Email,
                    PhoneNumber = registerDto.PhoneNumber,
                    UserType = registerDto.UserType,
                    FullName = registerDto.Fullname,
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
            if(user == null) return Ok(new { success = false, message = "User not found" });

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password,false);

            if(!result.Succeeded) return Ok(new { success = false, message = "Invalid Email or Password" });
            
            return Ok(user.toRegisterResponseDto(_tokenService.CreateToken(user))); ;
            //return Ok(new { success = false, message = user.toRegisterResponseDto(_tokenService.CreateToken(user)) }); ;
        }

        [HttpPost("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Extract email from JWT token
            var email = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email || c.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
                return Unauthorized("Email not found in token.");

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound("User not found.");

            // Generate a reset token
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Use the token to reset the password
            var result = await _userManager.ResetPasswordAsync(user, resetToken, dto.NewPassword);

            if (!result.Succeeded)
                return BadRequest(new
                {
                    message = "Password update failed",
                    errors = result.Errors.Select(e => e.Description)
                });

            return Ok(new { message = "Password updated successfully" });
        }
    }
}