using PokemonReviewApp.Dto.AuthDto;
using PokemonReviewApp.Dto.UserDto;
using PokemonReviewApp.Models;
using PokemonReviewApp.Result_Error.Result;

namespace PokemonReviewApp.Interfaces.Services
{
    public interface IAuthService
    {
        Task<Result> RegisterAsync(RegisterDto request);
        Task<IResult> LoginAsync(LoginDto request);
        Task<IResult> ValidateRefreshToken(RefreshTokenDto request);
        Task<IResult> LogoutAsync(string publicId);
    }

}
