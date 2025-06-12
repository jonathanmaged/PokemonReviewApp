using AutoMapper;
using OneOf;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Errors;
using PokemonReviewApp.Interfaces.Repository;
using PokemonReviewApp.Interfaces.Services;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Services
{
    public class PokemonService:IPokemonService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PokemonService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ICollection<PokemonDto>> GetPokemonsAsync()
        {
            var pokemons = await unitOfWork.PokemonRepository.GetAll();
            var pokemonsDto = mapper.Map<ICollection<PokemonDto>>(pokemons);
            return pokemonsDto;
        }
        public async Task<PokemonDto> GetPokemonByIdAsync(int id)
        {
            var pokemon = await unitOfWork.PokemonRepository.GetById(id);
            var pokemonDto = mapper.Map<PokemonDto>(pokemon);
            return pokemonDto;
        }

        public async Task<double> GetPokemonRatingAsync(int pokeId)
        {
            var rating = await unitOfWork.PokemonRepository.GetPokemonRatingAsync(pokeId);
            return rating;
        }
        //public async Task<OneOf<Pokemon, ConflictError, DatabaseError>> CreatePokemonAsync(PokemonDto pokemonDto)
        //{
        //    var pokemon = await pokemonRepository.GetPokemonByNameAsync(pokemonDto.Name);

        //    if (pokemon != null)
        //    {
        //        return new ConflictError("pokemon Already Exists");
        //    }

        //    pokemon = mapper.Map<Pokemon>(pokemonDto);

        //    var countryDto = new CountryDto { Name = countryName };
        //    var result = await countryService.CreateCountryAsync(countryDto);

        //    if (result.IsT2)
        //    {
        //        return new DatabaseError("Something Went wrong when saving in the database");
        //    }
        //    if (result.IsT0)
        //    {
        //        pokemon.Country = result.AsT0;
        //    }
        //    else if (result.IsT1)
        //    {
        //        pokemon.Country = await countryRepository.GetCountryByNameAsync(countryName);

        //    }

        //    var saved = await _pokemonRepository.CreatepokemonAsync(pokemon);
        //    if (!saved)
        //        return new DatabaseError("Something Went wrong when saving in the database");
        //    return pokemon;
        //}
    }
}
