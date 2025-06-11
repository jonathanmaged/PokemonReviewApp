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
    public class CountryService:ICountryService
    {
        private readonly ICountryRepository countryRepository;
        private readonly IMapper mapper;

        public CountryService(ICountryRepository countryRepository,IMapper mapper)
        {
            this.countryRepository = countryRepository;
            this.mapper = mapper;
        }

        public async Task<OneOf<Country, ConflictError, DatabaseError>> CreateCountryAsync(CountryDto countryDto)
        {
            var country = await countryRepository.GetCountryByNameAsync(countryDto.Name);

            if (country != null)
            {
                return new ConflictError("Country Already Exists");
            }
            country = mapper.Map<Country>(countryDto);

            var saved = await countryRepository.CreateCountryAsync(country);
            if (!saved)
            {
                return new DatabaseError("Something Went wrong when saving in the database");
            }
            return country;
        }
    }
}
    

