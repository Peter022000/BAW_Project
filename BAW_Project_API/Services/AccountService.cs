﻿using BAW_Project_API.Dtos;
using BAW_Project_API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BAW_Project_API.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(UserManager<IdentityUser> userManager, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AuthResult> ChangePasswordAsync(ChangePassword model)
        {
            var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
            var jwt = authorizationHeader?.Substring("Bearer ".Length).Trim();

            var handler = new JwtSecurityTokenHandler();
            var tokenToGetLogin = handler.ReadJwtToken(jwt);

            var claims = tokenToGetLogin.Claims.Select(claim => (claim.Type, claim.Value)).ToList();

            var userLogin = claims[0].Value;

            var user = await _userManager.FindByNameAsync(userLogin);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.OldPassword))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

                if (result.Succeeded)
                {
                    return new AuthResult
                    {
                        IsSuccess = true
                    };
                }
            }

            return new AuthResult
            {
                IsSuccess = false
            };
        }

        public async Task<AuthResult> LoginAsync(Login model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:ExpiryMinutes"]!)),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)),
                    SecurityAlgorithms.HmacSha256));

                return new AuthResult
                {
                    IsSuccess = true,
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                };
            }

            return new AuthResult { IsSuccess = false };
        }

        public async Task<IdentityResult> RegisterUserAsync(Register model)
        {
            var user = new IdentityUser { UserName = model.Username, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            return result;
        }

    }
}
