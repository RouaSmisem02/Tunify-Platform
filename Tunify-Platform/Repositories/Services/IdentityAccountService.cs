using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Tunify_Platform.Data;
using Tunify_Platform.Models;
using Tunify_Platform.Models.DTO;
using Tunify_Platform.Repositories.Interfaces;
namespace Tunify_Platform.Repositories.Services
{
    public class IdentityAccountService : IAccounts
    {
        private readonly TunifyDbContext _context;
        private readonly IPasswordHasher<Users> _passwordHasher;

        public IdentityAccountService(TunifyDbContext context, IPasswordHasher<Users> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<IActionResult> Register(RegisterDto registerDto, ModelStateDictionary modelState)
        {
            if (registerDto == null)
            {
                return new BadRequestObjectResult("Invalid user registration data.");
            }

            if (registerDto.Password != registerDto.ConfirmPassword)
            {
                modelState.AddModelError("ConfirmPassword", "Password and confirmation password do not match.");
                return new BadRequestObjectResult(modelState);
            }

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == registerDto.Username);
            if (existingUser != null)
            {
                modelState.AddModelError("Username", "Username already exists.");
                return new BadRequestObjectResult(modelState);
            }

            var user = new Users
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                Password = _passwordHasher.HashPassword(null, registerDto.Password) // Hash the password
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new OkObjectResult("User registered successfully.");
        }

        public async Task<Users> UserAuthentication(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null || _passwordHasher.VerifyHashedPassword(user, user.Password, password) != PasswordVerificationResult.Success)
            {
                return null; // or handle accordingly
            }

            return user; // Return the authenticated user object
        }

        public async Task<bool> LogOut(LogOutDto logOutDto)
        {
            if (logOutDto == null)
            {
                return false; // Return false if the logout data is invalid
            }

            // Implement your logout logic here
            // For example, if using ASP.NET Identity:
            // await _signInManager.SignOutAsync();

            // Example assuming session-based authentication:
            // HttpContext.Session.Clear();

            return true; // Return true to indicate successful logout
        }
    }
}
