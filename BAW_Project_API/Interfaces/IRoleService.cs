using BAW_Project_API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BAW_Project_API.Interfaces
{
    public interface IRoleService
    {
        Task<RoleResult> AddRoleAsync([FromBody] string role);
        Task<RoleResult> AssignRoleAsync(UserRole model);
    }
}
