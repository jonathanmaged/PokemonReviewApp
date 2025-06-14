using AutoMapper;
using OneOf;
using PokemonReviewApp.Dto.GetDto;
using PokemonReviewApp.Errors;
using PokemonReviewApp.Interfaces.Repository;
using PokemonReviewApp.Interfaces.Services;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        public async Task<ICollection<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await unitOfWork.CategoryRepository.GetAll();
            var categoriesDto = mapper.Map<ICollection<CategoryDto>>(categories);
            return categoriesDto;
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
        {
            var category = await unitOfWork.CategoryRepository.GetById(id);
            var categoryDto = mapper.Map<CategoryDto>(category);
            return categoryDto;

        }

        public async Task<CategoryDto?> GetCategoryByNameAsync(string name)
        {
            var category = await unitOfWork.CategoryRepository.GetCategoryByNameAsync(name);
            var categoryDto = mapper.Map<CategoryDto>(category);
            return categoryDto;
        }

        public async Task<ICollection<PokemonDto>> GetPokemonByCategoryAsync(int categoryId)
        {
            var pokemons = await unitOfWork.CategoryRepository.GetPokemonByCategoryAsync(categoryId);
            var pokemonDto = mapper.Map<List<PokemonDto>>(pokemons);
            return pokemonDto;

        }
        public async Task<OneOf<Category, ConflictError<Category>, DatabaseError>> CreateCategoryAsync(CategoryDto categoryDto)
        {
            var category = await unitOfWork.CategoryRepository.GetCategoryByNameAsync(categoryDto.Name);

            if (category != null)
            {
                return new ConflictError<Category>("Category Already Exists",category);
            }
            category = mapper.Map<Category>(categoryDto);

            unitOfWork.CategoryRepository.Add(category);

            var saved = await unitOfWork.Save();
            if (saved==0)
            {
                return new DatabaseError("Something Went wrong when saving in the database");
            }
            return category;
        }

        public async Task<OneOf<Category, NotFoundError, DatabaseError>> UpdateCategoryAsync(CategoryDto categoryDto)
        {
            var category = await unitOfWork.CategoryRepository.GetById(categoryDto.Id.Value);
            if (category is null)
                return new NotFoundError("category id provided doesnt exist in the database");

            mapper.Map(categoryDto, category);

            unitOfWork.CategoryRepository.Update(category);
            var saved = await unitOfWork.Save();
            if (saved == 0)
                return new DatabaseError("Couldnt save in the database");
            return category;
        }
    }
}
