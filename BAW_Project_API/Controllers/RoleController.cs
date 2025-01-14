using BAW_Project_API.Dtos;
using BAW_Project_API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BAW_Project_API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("add-role")]
        public async Task<IActionResult> AddRole([FromBody] string role)
        {
            var result = await _roleService.AddRoleAsync(role);

            if (result.IsSuccess)
            {
                return Ok(new { message = result.Message });
            }

            return BadRequest(result.Message);
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] UserRole model)
        {
            var result = await _roleService.AssignRoleAsync(model);

            if (result.IsSuccess)
            {
                return Ok(new { message = result.Message });
            }

            return BadRequest(result.Message);
        }
    }
}
