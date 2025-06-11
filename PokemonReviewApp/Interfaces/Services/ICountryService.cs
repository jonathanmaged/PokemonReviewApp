using OneOf;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Errors;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces.Services
{
    public interface ICountryService
    {
        Task<OneOf<Country, ConflictError, DatabaseError>> CreateCountryAsync(CountryDto countryDto);

    }
}
