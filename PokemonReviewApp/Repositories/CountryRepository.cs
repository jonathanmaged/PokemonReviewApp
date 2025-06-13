using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces.Repository;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
    public class CountryRepository : GenericRepository<Country>,ICountryRepository
    {
        private readonly DataContext context;
        public CountryRepository(DataContext context):base(context) 
        {
            this.context = context;
        }

        public async Task<Country> GetCountryByNameAsync(string name)
        {
            return await context.Countries.FirstOrDefaultAsync(c => c.Name.Trim().ToLower() == name.Trim().ToLower());

        }

        public async Task<Country> GetCountryByOwnerAsync(int ownerId)
        {
            return await context.Owners.Where(o => o.Id == ownerId)
                .Select(o => o.Country)
                .FirstOrDefaultAsync();
        }

    }
}