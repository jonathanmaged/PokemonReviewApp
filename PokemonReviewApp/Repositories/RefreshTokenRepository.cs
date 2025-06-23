using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces.Repository;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
    public class RefreshTokenRepository(DataContext context) :
        GenericRepository<RefreshToken>(context),
        IRefreshTokenRepository
    {
        public async Task<RefreshToken?> GetRefreshTokenAsync(string token)
        {
            return await context.RefreshTokens.Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Token == token);
        }
    }
}
