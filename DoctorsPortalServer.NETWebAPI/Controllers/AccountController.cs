using DoctorsPortalServer.NETWebAPI.Models;
using DoctorsPortalServer.NETWebAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DoctorsPortalServer.NETWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository accountRepository;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(IAccountRepository accountRepository, SignInManager<ApplicationUser> signInManager)
        {
            this.accountRepository = accountRepository;
            this.signInManager = signInManager;
        }

        [HttpPost("signup")] //createAccount
        public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel)
        {
            var Result = await accountRepository.SignUpAsync(signUpModel);
            if (Result.Succeeded)
            {
                return Ok(Result.Succeeded);
            }
            return Unauthorized();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SignInModel signInModel)
        {
            var result = await accountRepository.LoginAsync(signInModel);
            if(string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }
            return Ok(result);
        }

        [HttpGet("user")] //get currentUser by JWT_Token
        [Authorize]
        public async Task<IActionResult> CurrentUser()
        {
            var email = HttpContext.User.Claims.First().Value;
            if(email == null) return Unauthorized();
            var user = await accountRepository.GetUserByEmailAsync(email);
            if(user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return Ok(true);
        }

        [Authorize]
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await accountRepository.GetAllUserAsync();
            return Ok(users);
        }

        [Authorize]
        [HttpPut("{email}")]
        public async Task<IActionResult> UpdateUserRole([FromRoute] string email)
        {
            var updateRole = await accountRepository.UpdateUserRoleAsync(email);
            if(updateRole == null) return BadRequest();
            return Ok(updateRole);
        }
    }
}
