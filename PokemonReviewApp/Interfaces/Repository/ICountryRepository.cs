using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces.Repository
{
    public interface ICountryRepository
    {
        Task<ICollection<Country>> GetCountriesAsync();
        Task<Country?> GetCountryByIdAsync(int countryId);
        Task<Country?> GetCountryByNameAsync(string name);
        Task<Country?> GetCountryByOwnerAsync(int ownerId);
        Task<bool> CreateCountryAsync(Country country);
        Task<bool> SaveAsync();
        Task<bool> CountryExistsAsync(int countryId);
    }
}