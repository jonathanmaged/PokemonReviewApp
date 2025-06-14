using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    public class CountriesController : Controller
    {
        private readonly ICountryService countryService;

        public CountriesController(ICountryService countryService)
        {

            this.countryService = countryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            var countriesDto = await countryService.GetCountriesAsync();
            return Ok(countriesDto);
        }
        [HttpGet("{countryId}")]
        public async Task<IActionResult> GetCountry(int countryId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var countryDto = await countryService.GetCountryByIdAsync(countryId);

            if (countryDto == null)
                return NotFound();

            return Ok(countryDto);
        }
        [HttpGet("owner/{ownerId}")]
        public async Task<IActionResult> GetCountryOfAnOwner(int ownerId) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var countryDto = await countryService.GetCountryOfAnOwnerAsync(ownerId);

            if(countryDto == null)
                return NotFound();

            return Ok(countryDto);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCountry([FromBody] CountryDto countryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await countryService.CreateCountryAsync(countryDto);

            return result.Match<IActionResult>(
                    owner => Ok("Created successfully"),
                    conflictError => StatusCode(422, conflictError.Message),
                    databaseError => StatusCode(500, databaseError.Message)
                    );

        }
        [HttpPut]
        public async Task<IActionResult> UpdateCountry([FromBody] CountryDto countryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await countryService.UpdateCountryAsync(countryDto);
            return result.Match<IActionResult>(
                   pokemon => NoContent(),
                   notFoundError => StatusCode(404, notFoundError.Message),
                   databaseError => StatusCode(500, databaseError.Message)
                   );
        }

    }
}
