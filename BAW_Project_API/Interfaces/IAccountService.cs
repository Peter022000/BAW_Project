using BAW_Project_API.Dtos;
using Microsoft.AspNetCore.Identity;

namespace BAW_Project_API.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterUserAsync(Register model);
        Task<AuthResult> LoginAsync(Login model);
        Task<AuthResult> ChangePasswordAsync(ChangePassword model);
    }
}