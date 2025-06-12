using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces.Repository
{
    public interface ICountryRepository:IGenericRepository<Country>
    {
        Task<Country> GetCountryByNameAsync(string name);
        Task<Country> GetCountryByOwnerAsync(int ownerId);

    }
}