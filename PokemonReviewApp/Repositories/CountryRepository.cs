using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces.Repository;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;

        public CountryRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Country>> GetCountriesAsync()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<Country?> GetCountryByIdAsync(int countryId)
        {
            return await _context.Countries.FirstOrDefaultAsync(c => c.Id == countryId);
        }
        public async Task<Country?> GetCountryByNameAsync(string name)
        {
            return await _context.Countries.FirstOrDefaultAsync(c => c.Name == name);

        }

        public async Task<Country?> GetCountryByOwnerAsync(int ownerId)
        {
            return await _context.Owners.Where(o => o.Id == ownerId)
                .Select(o => o.Country)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CountryExistsAsync(int countryId)
        {
            return await _context.Countries.AnyAsync(c => c.Id == countryId);
        }

        public async Task<bool> CreateCountryAsync(Country country)
        {
            _context.Countries.Add(country);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0 ? true : false;

        }
    }
}