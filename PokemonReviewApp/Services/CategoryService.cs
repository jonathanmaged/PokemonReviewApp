using AutoMapper;
using OneOf;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Errors;
using PokemonReviewApp.Interfaces.Repository;
using PokemonReviewApp.Interfaces.Services;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoryService(ICategoryRepository categoryRepository,IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public async Task<OneOf<Category, ConflictError, DatabaseError>> CreateCategoryAsync(CategoryDto categoryDto)
        {
            var category = await categoryRepository.GetCategoryByNameAsync(categoryDto.Name);

            if (category != null)
            {
                return new ConflictError("Category Already Exists");
            }
            category = mapper.Map<Category>(categoryDto);

            var saved = await categoryRepository.CreateCategoryAsync(category);
            if (!saved)
            {
                return new DatabaseError("Something Went wrong when saving in the database");
            }
            return category;
        }
    }
}
