using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using Tunify_Platform.Models.DTO;
using Tunify_Platform.Repositories.Interfaces;

namespace Tunify_Platform.Repositories.Services
{
    public class IdentityAccountService : IAccounts
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public IdentityAccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Register a new user
        public async Task<IActionResult> Register(RegisterDto registerDto, ModelStateDictionary modelState)
        {
            var user = new IdentityUser { UserName = registerDto.Username, Email = registerDto.Email };
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                return new OkResult();
            }

            foreach (var error in result.Errors)
            {
                modelState.AddModelError(string.Empty, error.Description);
            }

            return new BadRequestObjectResult(modelState);
        }

        // Authenticate a user
        public async Task<IActionResult> UserAuthentication(string username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, false, false);

            if (result.Succeeded)
            {
                return new OkResult();
            }

            return new UnauthorizedResult();
        }

        // Log out a user
        public async Task<bool> LogOut(LogOutDto logOutDto)
        {
            try
            {
                await _signInManager.SignOutAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
