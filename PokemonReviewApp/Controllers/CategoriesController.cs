using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto.GetDto;
using PokemonReviewApp.Interfaces.Services;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {

        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
           
            var categoriesDto =  await categoryService.GetCategoriesAsync();
            return Ok(categoriesDto);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategory(int categoryId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryDto = await categoryService.GetCategoryByIdAsync(categoryId);

            if(categoryDto == null)
                return NotFound();

            return Ok(categoryDto);
        }

        [HttpGet("{categoryId}/pokemons")]
        public async Task<IActionResult> GetPokemonByCategory(int categoryId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemonDto = await categoryService.GetPokemonByCategoryAsync(categoryId);

            if (pokemonDto == null || !pokemonDto.Any())
                return NotFound();

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
