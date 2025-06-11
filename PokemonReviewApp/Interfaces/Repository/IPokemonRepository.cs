using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces.Repository
{
    public interface IPokemonRepository
    {
        Task<ICollection<Pokemon>> GetPokemonsAsync();
        Task<Pokemon?> GetPokemonByIdAsync(int id);
        Task<Pokemon?> GetPokemonByNameAsync(string name);
        Task<double> GetPokemonRatingAsync(int id);
        Task<bool> CreatePokemonAsync(Pokemon pokemon);
        Task<bool> SaveAsync();
        Task<bool> PokemonExistsAsync(int id);
    }
}