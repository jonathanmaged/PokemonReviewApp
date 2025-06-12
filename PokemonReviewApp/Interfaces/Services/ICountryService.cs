using OneOf;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Errors;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces.Services
{
    public interface ICountryService
    {
        public Task<ICollection<CountryDto>> GetCountriesAsync();
        public Task<CountryDto?> GetCountryByIdAsync(int id);
        public Task<CountryDto?> GetCountryOfAnOwnerAsync(int ownerId);
        public Task<OneOf<Country, ConflictError, DatabaseError>> CreateCountryAsync(CountryDto countryDto);

    }
}
