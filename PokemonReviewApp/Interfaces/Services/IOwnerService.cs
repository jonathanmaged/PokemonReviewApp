using OneOf;
using PokemonReviewApp.Dto.CreateDto;
using PokemonReviewApp.Dto.GetDto;
using PokemonReviewApp.Errors;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces.Services
{
    public interface IOwnerService
    {
        public Task<ICollection<OwnerDto>> GetOwnersAsync();
        public Task<OwnerDto?> GetOwnerByIdAsync(int id);
        public Task<ICollection<OwnerDto>> GetOwnersOfAPokemonAsync(int pokeId);
        public Task<ICollection<PokemonDto>> GetPokemonsByOwnerAsync(int ownerId);
        Task<OneOf<Owner, ConflictError<Owner>, DatabaseError,NotFoundError>> CreateOwnerAsync(CreateOwnerDto ownerDto, string countryName);
        public Task<OneOf<Owner, NotFoundError, DatabaseError>> UpdateOwnerAsync(OwnerDto ownerDto);
        public Task<OneOf<Owner, NotFoundError, DatabaseError>> DeleteOwnerAsync(int id);
    }
}
