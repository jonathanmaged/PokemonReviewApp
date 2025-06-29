using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PokemonReviewApp.Dto.AuthDto;
using PokemonReviewApp.Dto.UserDto;
using PokemonReviewApp.Interfaces.Services;
using PokemonReviewApp.Models;
using PokemonReviewApp.Result_Error.Result;

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
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await authService.RegisterAsync(request);
            if (result.IsSuccess) return Created();
            var error = result.Error;
            return StatusCode(error.StatusCode, error.ErrorList);
        }

        //Login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await authService.LoginAsync(request);
            if (!result.IsSuccess) return Unauthorized(result.Error?.Message);

            if(result is Result<TokenPairDto> typed)
                return Ok(typed.SuccessResponse?.Entity);

            return StatusCode(500);
        }

        //Refresh Token 
        [HttpPost("refresh-Token")]
        public async Task<IActionResult> ValidateRefreshToken(RefreshTokenDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await authService.ValidateRefreshToken(request);

            if (!result.IsSuccess) return Unauthorized(result.Error?.Message);

            if(result is Result<TokenPairDto> res)
                return Ok(res.SuccessResponse?.Entity);

            return StatusCode(500);

        }

        //Logout
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var publicUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = (Result)await authService.LogoutAsync(publicUserId);
            return Ok(result.SuccessResponse?.Message);
        }
        


    }
}
