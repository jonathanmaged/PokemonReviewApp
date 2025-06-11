using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces.Repository;
using PokemonReviewApp.Interfaces.Services;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper,ICategoryService categoryService)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
           
            var categories =  await _categoryRepository.GetCategoriesAsync();
            var categoriesDto = _mapper.Map<ICollection<CategoryDto>>(categories);


            return Ok(categoriesDto);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategory(int categoryId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);

            if(category == null)
                return NotFound();

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Ok(categoryDto);
        }

        [HttpGet("{categoryId}/pokemon")]
        public async Task<IActionResult> GetPokemonByCategory(int categoryId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemons = await _categoryRepository.GetPokemonByCategoryAsync(categoryId);

            if (pokemons ==null || !pokemons.Any())
                return NotFound();

            var pokemonDto = _mapper.Map<List<PokemonDto>>(pokemons);
            return Ok(pokemonDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto categoryDto)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            var result = await categoryService.CreateCategoryAsync(categoryDto);

            return result.Match<IActionResult>(
                    owner => Ok("Created successfully"),
                    conflictError => StatusCode(422, conflictError.Message),
                    databaseError => StatusCode(500, databaseError.Message)
                    );

        }
    }
}
