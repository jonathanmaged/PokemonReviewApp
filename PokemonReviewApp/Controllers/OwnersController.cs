using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto.CreateDto;
using PokemonReviewApp.Dto.GetDto;
using PokemonReviewApp.Errors;
using PokemonReviewApp.Interfaces.Repository;
using PokemonReviewApp.Interfaces.Services;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repositories;
using PokemonReviewApp.Services;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController:Controller
    {
        private readonly IOwnerService ownerService;

        public OwnersController(IOwnerService ownerService )
        {
            this.ownerService = ownerService;
        }
        [HttpGet]
        public async  Task<IActionResult> GetOwners() 
        {
            var ownersDto = await ownerService.GetOwnersAsync();
            return Ok(ownersDto);
        }

        [HttpGet("{ownerId}")]
        public async Task<IActionResult> GetOwner(int ownerId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ownerDto = await ownerService.GetOwnerByIdAsync(ownerId);

            if(ownerDto == null)
                return NotFound();


            return Ok(ownerDto);
        }

        [HttpGet("pokemon/{pokeId}")]
        public async Task<IActionResult> GetOwnersOfAPokemon(int pokeId) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ownersDto = await ownerService.GetOwnersOfAPokemonAsync(pokeId);
            return Ok(ownersDto);

        }

        [HttpGet("{ownerId}/pokemons")]
        public async Task<IActionResult> GetPokemonsByOwner(int ownerId) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemonsDto = await ownerService.GetPokemonsByOwnerAsync(ownerId);
            return Ok(pokemonsDto);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOwner(
            [FromBody] CreateOwnerDto createOwnerDto,
            [FromQuery] string countryName)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await ownerService.CreateOwnerAsync(createOwnerDto,countryName);

            return result.Match<IActionResult>(
                    owner => Ok("Created successfully"),
                    conflictError => StatusCode(422, conflictError.Message),
                    databaseError => StatusCode(500, databaseError.Message),
                    NotFoundError => StatusCode(404,NotFoundError.Message)
                    );

        }
        [HttpPut]
        public async Task<IActionResult> UpdateOwner([FromBody] OwnerDto ownerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await ownerService.UpdateOwnerAsync(ownerDto);
            return result.Match<IActionResult>(
                   pokemon => NoContent(),
                   notFoundError => StatusCode(404, notFoundError.Message),
                   databaseError => StatusCode(500, databaseError.Message)
                   );
        }

    }
}
