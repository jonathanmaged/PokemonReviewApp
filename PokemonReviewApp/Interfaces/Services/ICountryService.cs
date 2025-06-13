using OneOf;
using PokemonReviewApp.Dto.GetDto;
using PokemonReviewApp.Errors;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces.Services
{
    public interface ICountryService
    {
        public Task<ICollection<CountryDto>> GetCountriesAsync();
        public Task<CountryDto?> GetCountryByIdAsync(int id);
        public Task<CountryDto?> GetCountryOfAnOwnerAsync(int ownerId);
        public Task<OneOf<Country, ConflictError<Country>, DatabaseError>> CreateCountryAsync(CountryDto countryDto);

    }
}
