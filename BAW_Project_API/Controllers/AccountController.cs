using BAW_Project_API.Dtos;
using BAW_Project_API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BAW_Project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            var result = await _accountService.RegisterUserAsync(model);

            if (result.Succeeded)
            {
                return Ok(new { message = "User registered successfully" });
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var result = await _accountService.LoginAsync(model);

            if (result.IsSuccess)
            {
                return Ok(new { token = result.Token });
            }

            return Unauthorized();
        }

        [HttpPost("change_password")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword model)
        {
            var result = await _accountService.ChangePasswordAsync(model);

            if (result.IsSuccess)
            {
                return Ok(new { message = "Password changed successfully" });
            }

            return BadRequest("Password doesn't match");

        }
    }
}
