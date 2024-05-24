using DoctorsPortalServer.NETWebAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace DoctorsPortalServer.NETWebAPI.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUpModel signUpModel);
        Task<string> LoginAsync(SignInModel signInModel);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<List<ApplicationUser>> GetAllUserAsync();
        Task<ApplicationUser> UpdateUserRoleAsync(string userId);
    }
}
