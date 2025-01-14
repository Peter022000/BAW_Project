using BAW_Project_API.Dtos;
using BAW_Project_API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BAW_Project_API.Services
{
    public class RoleService : IRoleService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public RoleService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<RoleResult> AddRoleAsync([FromBody] string role)
        {
            if (await _roleManager.RoleExistsAsync(role))
            {
                return new RoleResult { IsSuccess = false, Message = "Role already exists" };
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(role));
            if (result.Succeeded)
            {
                return new RoleResult { IsSuccess = true, Message = "Role added successfully" };
            }

            return new RoleResult
            {
                IsSuccess = false,
                Message = string.Join(", ", result.Errors.Select(e => e.Description))
            };
        }

        public async Task<RoleResult> AssignRoleAsync(UserRole model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return new RoleResult { IsSuccess = false, Message = "User not found" };
            }

            var result = await _userManager.AddToRoleAsync(user, model.Role);
            if (result.Succeeded)
            {
                return new RoleResult { IsSuccess = true, Message = "Role assigned successfully" };
            }

            return new RoleResult
            {
                IsSuccess = false,
                Message = string.Join(", ", result.Errors.Select(e => e.Description))
            };
        }
    }
}
