using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces.Repository;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.OrderBy(c => c.Id).ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Category?> GetCategoryByNameAsync(string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<ICollection<Pokemon>> GetPokemonByCategoryAsync(int categoryId)
        {
            // added a forign key CategoryId to the pokemon entity in order
            // not for the sql to make inner join for pokemon with category 
            var pokemons = await _context.Pokemons
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();

            return pokemons;
        }

        public async Task<bool> CategoryExistsAsync(int id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> CreateCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0 ? true : false;
        }

    }
}