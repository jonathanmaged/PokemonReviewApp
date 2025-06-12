using OneOf;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Errors;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces.Services
{
    public interface IPokemonService
    {
        public Task<ICollection<PokemonDto>> GetPokemonsAsync();
        public Task<PokemonDto> GetPokemonByIdAsync(int id);
        public Task<double> GetPokemonRatingAsync(int pokeId);
        //Task<OneOf<Pokemon, ConflictError, DatabaseError>> CreatePokemonAsync(PokemonDto pokemonDto);

    }
}
