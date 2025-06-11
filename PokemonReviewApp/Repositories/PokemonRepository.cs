using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces.Repository;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;

        public PokemonRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task<ICollection<Pokemon>> GetPokemonsAsync()
        {
            return await _context.Pokemons.OrderBy(p => p.Id).ToListAsync();
        }

        public async Task<Pokemon?> GetPokemonByIdAsync(int id)
        {
            return await _context.Pokemons.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Pokemon?> GetPokemonByNameAsync(string name)
        {
            return await _context.Pokemons.FirstOrDefaultAsync(c => c.Name == name);

        }
        public async Task<Pokemon?> GetPokemonAsync(string name)
        {
            return await _context.Pokemons.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<double> GetPokemonRatingAsync(int id)
        {
            var reviews = _context.Reviews.Where(r => r.PokemonId == id);
            if (await reviews.CountAsync() == 0)
                return 0;
            return await reviews.AverageAsync(r => r.Rating);
        }

        public async Task<bool> PokemonExistsAsync(int id)
        {
            return await _context.Pokemons.AnyAsync(p => p.Id == id);
        }

        public async Task<bool> CreatePokemonAsync(Pokemon pokemon)
        {
            _context.Pokemons.Add(pokemon);
            return await SaveAsync();

        }

        public async Task<bool> SaveAsync()
        {
            var rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0 ? true : false;
        }
    }
}