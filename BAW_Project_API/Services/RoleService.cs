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

        public async Task<RoleResult> AddRole([FromBody] string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(role));
                if (result.Succeeded)
                {
                    return new RoleResult { IsSuccess = true, Message ="Role added successfully" });
                }

                return new RoleResult
                {
                    IsSuccess = true,
                    Message = result.Errors
                };

            }

            return BadRequest("Role already exists");

        }

        public Task<RoleResult> AssignRole([FromBody] UserRole model)
        {
            throw new NotImplementedException();
        }
    }
}
