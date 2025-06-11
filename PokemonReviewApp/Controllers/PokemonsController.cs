using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces.Repository;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonsController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;

        public PokemonsController(IPokemonRepository pokemonRepository, IMapper mapper)
        {
            _pokemonRepository = pokemonRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPokemons()
        {
            var pokemons = await _pokemonRepository.GetPokemonsAsync();
            var pokemonsDto = _mapper.Map<ICollection<PokemonDto>>(pokemons);
          
            return Ok(pokemonsDto);
        }

        [HttpGet("{pokeId}")]

        public async Task<IActionResult> GetPokemon(int pokeId)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemon =await _pokemonRepository.GetPokemonByIdAsync(pokeId);

            if (pokemon == null)
                return NotFound();

            var pokemonDto = _mapper.Map<PokemonDto>(pokemon);
            return Ok(pokemonDto);

        }
        [HttpGet("{pokeId}/rating")]
        public async Task<IActionResult> GetPokemonRating(int pokeId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //if (!_pokemonRepository.PokemonExists(pokeId))
            //    return NotFound();

            var rating =await _pokemonRepository.GetPokemonRatingAsync(pokeId);
            return Ok(rating);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePokemon([FromBody] PokemonDto pokemonDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemon = await _pokemonRepository.GetPokemonByNameAsync(pokemonDto.Name);

            if (pokemon != null)
            {
                ModelState.AddModelError("", "Pokemon Already Exists");
                return StatusCode(422, ModelState);
            }
            pokemon = _mapper.Map<Pokemon>(pokemonDto);

            var saved = await _pokemonRepository.CreatePokemonAsync(pokemon);
            if (!saved)
            {
                ModelState.AddModelError("", "Something Went wrong when saving in the database");
                return StatusCode(500, ModelState);
            }
            return Ok("Created Succesfully");

        }

    }
}
