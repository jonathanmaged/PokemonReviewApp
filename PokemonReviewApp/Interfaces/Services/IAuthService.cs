using PokemonReviewApp.Dto;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse> RegisterAsync(UserDto request);
        Task<ServiceResponse> LoginAsync(UserDto request);
    }
}
