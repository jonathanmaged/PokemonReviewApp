using OneOf;
using PokemonReviewApp.Dto.CreateDto;
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
        public Task<OneOf<Country, ConflictError<Country>, DatabaseError>> CreateCountryAsync(CreateCountryDto countryDto);
        public Task<OneOf<Country, NotFoundError, DatabaseError>> UpdateCountryAsync(CountryDto countryDto);
        public Task<OneOf<Country, NotFoundError, DatabaseError>> DeleteCountryAsync(int id);

    }
}
