using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PokemonReviewApp.Dto.AuthDto;
using PokemonReviewApp.Dto.UserDto;
using PokemonReviewApp.Interfaces.Services;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(
        IAuthService authService) : ControllerBase
    {
        // Register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto request)
        {
            var response = await authService.RegisterAsync(request);
            if (response.StatusCode == 400)
                return BadRequest(new { response.Entity });
            return StatusCode(response.StatusCode, response.StatusMessage);
        }

        //Login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto request)
        {
            var result = await authService.LoginAsync(request);
            if (result.IsSuccess) return Ok(result.Entity);
            return Unauthorized(result.Error.Message); 

        }

        //Refresh Token 
        [HttpPost("refresh-Token")]
        public async Task<IActionResult> ValidateRefreshToken(RefreshTokenDto request)
        {
            var result = await authService.ValidateRefreshToken(request);
            if (result.IsSuccess) return Ok(result.Entity);
            return Unauthorized(result.Error.Message);
        }

        //Logout
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var publicUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var response = await authService.LogoutAsync(publicUserId);
            return StatusCode(response.StatusCode, response.StatusMessage);
        }
        


    }
}
