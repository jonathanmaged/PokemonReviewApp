using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces.Repository
{
    public interface IPokemonRepository:IGenericRepository<Pokemon>
    {
        Task<Pokemon?> GetPokemonByNameAsync(string name);
        Task<double> GetPokemonRatingAsync(int id);
    }
}