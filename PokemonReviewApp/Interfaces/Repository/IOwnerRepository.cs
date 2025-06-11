using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces.Repository
{
    public interface IOwnerRepository
    {
        Task<ICollection<Owner>> GetOwnersAsync();
        Task<Owner?> GetOwnerByIdAsync(int ownerId);
        Task<Owner?> GetOwnerByNameAsync(string lastname);
        Task<ICollection<Owner>> GetOwnersOfAPokemonAsync(int pokeId);
        Task<ICollection<Pokemon>> GetPokemonsByOwnerAsync(int ownerId);
        Task<bool> CreateOwnerAsync(Owner owner);
        Task<bool> SaveAsync();
        Task<bool> OwnerExistsAsync(int ownerId);
    }
}