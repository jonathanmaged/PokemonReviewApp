using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PokemonReviewApp.Dto.UserDto;
using PokemonReviewApp.Interfaces.Services;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto request)
        {
            if (!ModelState.IsValid)  return BadRequest(ModelState); 

            var response = await authService.RegisterAsync(request);
            return StatusCode(response.StatusCode, response.StatusMessage);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDto request)
        {
            if (!ModelState.IsValid)  return BadRequest(ModelState);
            var response = await authService.LoginAsync(request);
            var tokens = (TokenPairDto) response.Entity!;
            return StatusCode(response.StatusCode, new { tokens.Token, tokens.RefreshToken });

        }
        [HttpPost("refreshToken")]
        public async Task<IActionResult> ValidateRefreshToken(RefreshTokenRequestDto request)
        { 
            var response = await authService.ValidateRefreshToken(request);
            return StatusCode(response.StatusCode, response.StatusMessage);
        }


    }
}
