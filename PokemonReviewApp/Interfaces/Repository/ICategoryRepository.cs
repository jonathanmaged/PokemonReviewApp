using System.Collections.ObjectModel;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces.Repository
{
    public interface ICategoryRepository
    {
        Task<ICollection<Category>> GetCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<Category?> GetCategoryByNameAsync(string name);
        Task<ICollection<Pokemon>> GetPokemonByCategoryAsync(int categoryId);
        Task<bool> CreateCategoryAsync(Category category);
        Task<bool> SaveAsync();
        Task<bool> CategoryExistsAsync(int id);
    }
}