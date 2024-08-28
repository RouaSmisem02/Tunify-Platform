using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tunify_Platform.Models;
using Tunify_Platform.Models.DTO;
using Tunify_Platform.Repositories.Interfaces;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Tunify_Platform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccounts _IAccounts;
        private readonly IConfiguration _configuration;

        public AccountController(IAccounts context, IConfiguration configuration)
        {
            _IAccounts = context;
            _configuration = configuration;
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

            var user = await _IAccounts.UserAuthentication(loginDto.Username, loginDto.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            // Generate JWT Token
            var token = GenerateJwtToken(user);

            return Ok(new { token });
        }

        // Generate JWT Token
        private string GenerateJwtToken(Users user)  // Assuming your user class is 'Users'
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role) // Ensure 'Role' exists in your 'Users' class
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
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
