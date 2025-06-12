using Microsoft.AspNetCore.Mvc;
using OneOf;
using PokemonReviewApp.Dto;
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
        public Task<OneOf<Category, ConflictError, DatabaseError>> CreateCategoryAsync(CategoryDto categoryDto);
    }
}
