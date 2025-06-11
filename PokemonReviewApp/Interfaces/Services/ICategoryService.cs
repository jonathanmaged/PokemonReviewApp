using OneOf;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Errors;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces.Services
{
    public interface ICategoryService
    {
        public Task<OneOf<Category, ConflictError, DatabaseError>> CreateCategoryAsync(CategoryDto categoryDto);
    }
}
