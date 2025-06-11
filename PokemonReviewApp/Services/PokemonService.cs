using AutoMapper;
using OneOf;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Errors;
using PokemonReviewApp.Interfaces.Repository;
using PokemonReviewApp.Interfaces.Services;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Services
{
    public class PokemonService:IPokemonService
    {
        private readonly IPokemonRepository pokemonRepository;
        private readonly IMapper mapper;

        public PokemonService(IPokemonRepository pokemonRepository,IMapper mapper)
        {
            this.pokemonRepository = pokemonRepository;
            this.mapper = mapper;
        }

        public Task<OneOf<Pokemon, ConflictError, DatabaseError>> CreatePokemonAsync(PokemonDto pokemonDto)
        {
            throw new NotImplementedException();
        }
    }
}
