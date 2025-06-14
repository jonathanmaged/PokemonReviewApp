using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Dto.CreateDto;
using PokemonReviewApp.Dto.GetDto;
using PokemonReviewApp.Interfaces.Repository;
using PokemonReviewApp.Interfaces.Services;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repositories;
using PokemonReviewApp.Services;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonsController : Controller
    {
        private readonly IPokemonService pokemonService;

        public PokemonsController(IPokemonService pokemonService)
        {
            this.pokemonService = pokemonService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPokemons()
        {
            var pokemonsDto = await pokemonService.GetPokemonsAsync();
            return Ok(pokemonsDto);
        }

        [HttpGet("{pokeId}")]

        public async Task<IActionResult> GetPokemon(int pokeId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemonDto = await pokemonService.GetPokemonByIdAsync(pokeId);

            if (pokemonDto == null)
                return NotFound();

            return Ok(pokemonDto);

        }
        [HttpGet("{pokeId}/rating")]
        public async Task<IActionResult> GetPokemonRating(int pokeId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var rating = await pokemonService.GetPokemonRatingAsync(pokeId);
            return Ok(rating);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePokemon([FromBody] CreatePokemonDto createPokemonDto,
           [FromQuery] string categoryName)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await pokemonService.CreatePokemonAsync(createPokemonDto, categoryName);

            return result.Match<IActionResult>(
                    owner => Ok("Created successfully"),
                    conflictError => StatusCode(422, conflictError.Message),
                    databaseError => StatusCode(500, databaseError.Message)
                    );

        }
        [HttpPut]
        public async Task<IActionResult> UpdatePokemon([FromBody] PokemonDto pokemonDto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await pokemonService.UpdatePokemonAsync(pokemonDto);
            return result.Match<IActionResult>(
                   pokemon => NoContent(),
                   notFoundError => StatusCode(404, notFoundError.Message),
                   databaseError => StatusCode(500, databaseError.Message)
                   );
        }
        [HttpDelete("{pokemonId}")]
        public async Task<IActionResult> DeletePokemon(int pokemonId) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await pokemonService.DeletePokemonAsync(pokemonId);

            return result.Match<IActionResult>(
                   pokemon => NoContent(),
                   notFoundError => StatusCode(404, notFoundError.Message),
                   databaseError => StatusCode(500, databaseError.Message)
                   );
        }


    }
}
