using Microsoft.AspNetCore.Mvc.ModelBinding;
using Tunify_Platform.Models.DTO;
using Tunify_Platform.Models; // Ensure you have a using directive for your Users model
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Tunify_Platform.Repositories.Interfaces
{
    public interface IAccounts
    {
        // Register a new user
        Task<IActionResult> Register(RegisterDto registerDto, ModelStateDictionary modelState);

        // Authenticate a user and return a Users object instead of IActionResult
        Task<Users> UserAuthentication(string username, string password);

        // Log out a user
        Task<bool> LogOut(LogOutDto logOutDto);
    }
}
