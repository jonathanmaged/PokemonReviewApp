using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces.Repository;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
    public class CategoryRepository :GenericRepository<Category>,ICategoryRepository
    {
        private readonly DataContext context;
        public CategoryRepository(DataContext context):base(context) 
        {           
            this.context = context;
        }

        public async Task<Category?> GetCategoryByNameAsync(string name)
        {
            return await context.Categories.FirstOrDefaultAsync(c => c.Name.Trim().ToLower() == name.Trim().ToLower());
        }

        public async Task<ICollection<Pokemon>> GetPokemonByCategoryAsync(int categoryId)
        {
            var pokemons = await context.Pokemons
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();

            return pokemons;
        }

    }
}