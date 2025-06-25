using PokemonReviewApp.Dto.AuthDto;
using PokemonReviewApp.Dto.UserDto;
using PokemonReviewApp.Models;
using PokemonReviewApp.Result_Error.Result;

namespace PokemonReviewApp.Interfaces.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse> RegisterAsync(RegisterDto request);
        Task<Result<TokenPairDto>> LoginAsync(LoginDto request);
        Task<Result<TokenPairDto>> ValidateRefreshToken(RefreshTokenDto request);
        Task<ServiceResponse> LogoutAsync(string publicId);
    }

}
