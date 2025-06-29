using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto.CreateDto;
using PokemonReviewApp.Dto.GetDto;
using PokemonReviewApp.Interfaces.Services;
using PokemonReviewApp.Services;

namespace PokemonReviewApp.Controllers
{
    
    [Authorize]
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
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto categoryDto)
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

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await categoryService.UpdateCategoryAsync(categoryDto);
            return result.Match<IActionResult>(
                   pokemon => NoContent(),
                   notFoundError => StatusCode(404, notFoundError.Message),
                   databaseError => StatusCode(500, databaseError.Message)
                   );
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await categoryService.DeleteCategoryAsync(categoryId);

            return result.Match<IActionResult>(
                   category => NoContent(),
                   notFoundError => StatusCode(404, notFoundError.Message),
                   databaseError => StatusCode(500, databaseError.Message)
                   );
        }

    }
}
