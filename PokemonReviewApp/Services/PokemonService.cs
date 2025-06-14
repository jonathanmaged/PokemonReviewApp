using AutoMapper;
using OneOf;
using PokemonReviewApp.Dto.CreateDto;
using PokemonReviewApp.Dto.GetDto;
using PokemonReviewApp.Errors;
using PokemonReviewApp.Interfaces.Repository;
using PokemonReviewApp.Interfaces.Services;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Services
{
    public class PokemonService:IPokemonService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public PokemonService(IUnitOfWork unitOfWork,ICategoryService categoryService, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.categoryService = categoryService;
            this.mapper = mapper;
        }
        public async Task<ICollection<PokemonDto>> GetPokemonsAsync()
        {
            var pokemons = await unitOfWork.PokemonRepository.GetAll();
            var pokemonsDto = mapper.Map<ICollection<PokemonDto>>(pokemons);
            return pokemonsDto;
        }
        public async Task<PokemonDto> GetPokemonByIdAsync(int id)
        {
            var pokemon = await unitOfWork.PokemonRepository.GetById(id);
            var pokemonDto = mapper.Map<PokemonDto>(pokemon);
            return pokemonDto;
        }

        public async Task<double> GetPokemonRatingAsync(int pokeId)
        {
            var rating = await unitOfWork.PokemonRepository.GetPokemonRatingAsync(pokeId);
            return rating;
        }
        public async Task<OneOf<Pokemon, ConflictError<Pokemon>, DatabaseError>> CreatePokemonAsync(CreatePokemonDto createPokemonDto, string categoryName)
        {
            var conflictCheck = await CheckConflictAsync(createPokemonDto.Name);
            if (conflictCheck is not null) { return conflictCheck; }
            
            var pokemon = mapper.Map<Pokemon>(createPokemonDto);

            var DatabaseError = await HandleCategoryAssignmentAsync(categoryName, pokemon);
            if (DatabaseError is not null) { return DatabaseError; }

            unitOfWork.PokemonRepository.Add(pokemon);
            var saved = await unitOfWork.Save();
            if (saved == 0) return new DatabaseError("Something Went wrong when saving in the database");
            return pokemon;
        }
        private async Task<ConflictError<Pokemon>?> CheckConflictAsync(string name)
        {
            var pokemon = await unitOfWork.PokemonRepository.GetPokemonByNameAsync(name);

            if (pokemon != null)
            {
                return new ConflictError<Pokemon>("pokemon Already Exists", null);
            }
            return null;
        }
        private async Task<DatabaseError?> HandleCategoryAssignmentAsync(string categoryName, Pokemon pokemon)
        {

            var categoryDto = new CreateCategoryDto { Name = categoryName };
            var result = await categoryService.CreateCategoryAsync(categoryDto);

            if (result.IsT2)
            {
                return new DatabaseError("Something Went wrong when saving in the database");
            }
            if (result.IsT0)
            {
                pokemon.Category = result.AsT0;
            }
            else if (result.IsT1)
            {
                pokemon.Category = result.AsT1.Entity;

            }
            return null;
        }

        public async Task<OneOf<Pokemon,NotFoundError,DatabaseError>> UpdatePokemonAsync(PokemonDto pokemonDto)
        {

            var category = await unitOfWork.CategoryRepository.GetById(pokemonDto.CategoryId.Value);
            if (category is null)
                return new NotFoundError("category id provided doesnt exist in the database");
            
            var pokemon = await unitOfWork.PokemonRepository.GetById(pokemonDto.Id.Value);
            if (pokemon is null)
                return new NotFoundError("pokemon id provided doesnt exist in the database");

            mapper.Map(pokemonDto,pokemon);

            unitOfWork.PokemonRepository.Update(pokemon);
            var saved = await unitOfWork.Save();
            if(saved==0)
                return new DatabaseError("Couldnt save in the database");
            return pokemon;
        }

        public async Task<OneOf<Pokemon, NotFoundError, DatabaseError>> DeletePokemonAsync(int id)
        {
            var pokemon = await unitOfWork.PokemonRepository.GetById(id);
            if (pokemon is null) return new NotFoundError("Pokemon with that id doesnt exist");

            unitOfWork.PokemonRepository.Delete(pokemon);
            var saved = await unitOfWork.Save();
            if(saved==0) return new DatabaseError("Error Happen When Deleting Entity From The Database");
            return pokemon;
        }
    }
}
