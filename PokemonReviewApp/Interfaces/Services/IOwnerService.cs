using OneOf;
using PokemonReviewApp.Dto;
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
        Task<OneOf<Owner, ConflictError, DatabaseError>> CreateOwnerAsync(OwnerDto ownerDto, string countryName);
    }
}
