using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
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
            if(!ModelState.IsValid)
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

            var rating =await pokemonService.GetPokemonRatingAsync(pokeId);
            return Ok(rating);
        }
        //[HttpPost]
        //public async Task<IActionResult> CreatePokemon([FromBody] PokemonDto pokemonDto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var result = await pokemonService.CreatePokemonAsync(pokemonDto);

        //    return result.Match<IActionResult>(
        //            owner => Ok("Created successfully"),
        //            conflictError => StatusCode(422, conflictError.Message),
        //            databaseError => StatusCode(500, databaseError.Message)
        //            );
        
        //}

    }
}
