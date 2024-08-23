using Microsoft.AspNetCore.Mvc.ModelBinding;
using Tunify_Platform.Models.DTO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Tunify_Platform.Repositories.Interfaces
{
    public interface IAccounts
    {
        // Register a new user
        Task<IActionResult> Register(RegisterDto registerDto, ModelStateDictionary modelState);

        // Authenticate a user
        Task<IActionResult> UserAuthentication(string username, string password);

        // Log out a user
        Task<bool> LogOut(LogOutDto logOutDto);
    }
}
