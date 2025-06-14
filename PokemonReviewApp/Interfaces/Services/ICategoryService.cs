using Microsoft.AspNetCore.Mvc;
using OneOf;
using PokemonReviewApp.Dto.CreateDto;
using PokemonReviewApp.Dto.GetDto;
using PokemonReviewApp.Errors;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces.Services
{
    public interface ICategoryService
    {
        public Task<ICollection<CategoryDto>> GetCategoriesAsync();
        public Task<CategoryDto?> GetCategoryByIdAsync(int id);
        public Task<CategoryDto?> GetCategoryByNameAsync(string name);
        public Task<ICollection<PokemonDto>> GetPokemonByCategoryAsync(int categoryId);
        public Task<OneOf<Category, ConflictError<Category>, DatabaseError>> CreateCategoryAsync(CreateCategoryDto categoryDto);
        public Task<OneOf<Category, NotFoundError, DatabaseError>> UpdateCategoryAsync(CategoryDto categoryDto);
        public Task<OneOf<Category, NotFoundError, DatabaseError>> DeleteCategoryAsync(int id);
    }
}
