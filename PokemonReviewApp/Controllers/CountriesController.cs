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
    public class CountriesController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        private readonly ICountryService countryService;

        public CountriesController(ICountryRepository countryRepository, IMapper mapper,ICountryService countryService)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
            this.countryService = countryService;
        }
        [HttpGet]
        public IActionResult GetCountries()
        {
            var countries = _countryRepository.GetCountriesAsync();
            var countriesDto = _mapper.Map<List<CountryDto>>(countries);

            return Ok(countriesDto);
        }
        [HttpGet("{countryId}")]
        public IActionResult GetCountry(int countryId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var country = _countryRepository.GetCountryByIdAsync(countryId);

            if (country == null)
                return NotFound();

            var countryDto = _mapper.Map<CountryDto>(country);
            return Ok(countryDto);
        }
        [HttpGet("owner/{ownerId}")]
        public async Task<IActionResult> GetCountryOfAnOwner(int ownerId) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var country = await _countryRepository.GetCountryByOwnerAsync(ownerId);

            if(country == null)
                return NotFound();

            var countryDto = _mapper.Map<CountryDto>(country);
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

    }
}
