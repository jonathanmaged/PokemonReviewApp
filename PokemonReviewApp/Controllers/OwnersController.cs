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
    public class OwnersController:Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IOwnerService ownerService;
        private readonly IMapper _mapper;

        public OwnersController(IOwnerRepository ownerRepository,IOwnerService ownerService ,IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            this.ownerService = ownerService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetOwners() 
        {
            var owners = _ownerRepository.GetOwnersAsync();
            var ownersDto = _mapper.Map<List<OwnerDto>>(owners);
            return Ok(ownersDto);
        }

        [HttpGet("{ownerId}")]
        public IActionResult GetOwner(int ownerId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var owner = _ownerRepository.GetOwnerByIdAsync(ownerId);

            if(owner == null)
                return NotFound();

            var ownerDto = _mapper.Map<OwnerDto>(owner);

            return Ok(ownerDto);
        }

        [HttpGet("pokemon/{pokeId}")]
        public async Task<IActionResult> GetOwnersOfAPokemon(int pokeId) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var owners = await _ownerRepository.GetOwnersOfAPokemonAsync(pokeId);
            var ownersDto = _mapper.Map<List<OwnerDto>>(owners);
            return Ok(ownersDto);

        }

        [HttpGet("{ownerId}/pokemons")]
        public async Task<IActionResult> GetPokemonsByOwner(int ownerId) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemons = await _ownerRepository.GetPokemonsByOwnerAsync(ownerId);
            var pokemonsDto = _mapper.Map<List<PokemonDto>>(pokemons);
            return Ok(pokemonsDto);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOwner([FromBody] OwnerDto ownerDto,[FromQuery] string countryName)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await ownerService.CreateOwnerAsync(ownerDto,countryName);

            return result.Match<IActionResult>(
                    owner => Ok("Created successfully"),
                    conflictError => StatusCode(422, conflictError.Message),
                    databaseError => StatusCode(500, databaseError.Message)
                    );

        }

    }
}
