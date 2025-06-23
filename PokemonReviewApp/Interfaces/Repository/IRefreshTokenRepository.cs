using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces.Repository
{
    public interface IRefreshTokenRepository:IGenericRepository<RefreshToken>
    {
        Task<RefreshToken?> GetRefreshTokenAsync(string token);
    }
}
