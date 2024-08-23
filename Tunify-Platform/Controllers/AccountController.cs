using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tunify_Platform.Models;
using Tunify_Platform.Models.DTO;
using Tunify_Platform.Repositories.Interfaces;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace Tunify_Platform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccounts _IAccounts;

        public AccountController(IAccounts context)
        {
            _IAccounts = context;
        }

        // Register
        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _IAccounts.Register(registerDto, ModelState);

            if (result == null)
            {
                return Unauthorized();
            }

            return Ok(result);
        }

        // Login
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _IAccounts.UserAuthentication(loginDto.Username, loginDto.Password);

            if (result == null)
            {
                return Unauthorized();
            }

            return Ok(result);
        }

        // Logout
        [HttpPost("LogOut")]
        public async Task<ActionResult> LogOut(LogOutDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _IAccounts.LogOut(user);

            if (!result)
            {
                return BadRequest("Logout failed.");
            }

            return Ok("Logout successful.");
        }
    }
}
