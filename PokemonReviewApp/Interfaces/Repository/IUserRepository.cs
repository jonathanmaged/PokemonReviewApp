using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces.Repository
{
    public interface IUserRepository:IGenericRepository<User>
    {
        Task<bool> IsUserExistAsync(string username);
        Task<User?> GetUserByUserNameAsNoTracking(string username);
    }
}
