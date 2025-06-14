using OneOf;
using PokemonReviewApp.Dto.CreateDto;
using PokemonReviewApp.Dto.GetDto;
using PokemonReviewApp.Errors;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces.Services
{
    public interface IPokemonService
    {
        public Task<ICollection<PokemonDto>> GetPokemonsAsync();
        public Task<PokemonDto> GetPokemonByIdAsync(int id);
        public Task<double> GetPokemonRatingAsync(int pokeId);
        Task<OneOf<Pokemon, ConflictError<Pokemon>, DatabaseError>> CreatePokemonAsync(CreatePokemonDto createPokemonDto,string categoryName);
        public Task<OneOf<Pokemon, NotFoundError, DatabaseError>> UpdatePokemonAsync(PokemonDto pokemonDto);
        public Task<OneOf<Pokemon, NotFoundError, DatabaseError>> DeletePokemonAsync(int id);


    }
}
