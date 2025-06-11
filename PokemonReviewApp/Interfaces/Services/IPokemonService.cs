using OneOf;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Errors;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces.Services
{
    public interface IPokemonService
    {
        Task<OneOf<Pokemon, ConflictError, DatabaseError>> CreatePokemonAsync(PokemonDto pokemonDto);

    }
}
