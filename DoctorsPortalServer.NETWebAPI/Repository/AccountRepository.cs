using DoctorsPortalServer.NETWebAPI.Data;
using DoctorsPortalServer.NETWebAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DoctorsPortalServer.NETWebAPI.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly DoctorsPortalContext context;

        public AccountRepository(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IConfiguration configuration,
            DoctorsPortalContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.context = context;
        }

        //createUser
        public async Task<IdentityResult> SignUpAsync(SignUpModel signUpModel)
        {
            var user = new ApplicationUser()
            {
                FirstName = signUpModel.FirstName,
                LastName = signUpModel.LastName,
                Email = signUpModel.Email,
                UserName = signUpModel.Email,
                Role = "user",
            };
            return await userManager.CreateAsync(user, signUpModel.Password);
        }

        //Login
        public async Task<string> LoginAsync(SignInModel signInModel)
        {
            var result = await signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password,false,false);
            if(!result.Succeeded)
            {
                return null;
            }
            var authClaims = new List<Claim>
            { 
                new Claim(ClaimTypes.Name, signInModel.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:Secret"]));

            //new generate token
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                );
            return  new JwtSecurityTokenHandler().WriteToken(token);
        }

        //GetUserByEmail
        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            return user;
        }

        //GetAllUser
        public async Task<List<ApplicationUser>> GetAllUserAsync()
        {
            var users = await userManager.Users.Select(user => new ApplicationUser {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.Email,
                Role = user.Role,

            }).ToListAsync();
            return users;

        }

        //MakeUserToAdmin
        public async Task<ApplicationUser> UpdateUserRoleAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if(user == null)
            {
                throw new InvalidOperationException("user not found");
            }
            user.Role = "admin";
            
            context.ApplicationUser.Update(user);
            await context.SaveChangesAsync();

            return user;
        }
    }
}
