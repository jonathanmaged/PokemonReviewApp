using System.Collections.ObjectModel;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces.Repository
{
    public interface ICategoryRepository:IGenericRepository<Category>
    {

        Task<Category?> GetCategoryByNameAsync(string name);
        Task<ICollection<Pokemon>> GetPokemonByCategoryAsync(int categoryId);

    }
}