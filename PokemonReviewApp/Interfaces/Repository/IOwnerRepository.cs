using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces.Repository
{
    public interface IOwnerRepository:IGenericRepository<Owner>
    {
        Task<Owner?> GetOwnerByNameAsync(string lastname);
        Task<ICollection<Owner>> GetOwnersOfAPokemonAsync(int pokeId);
        Task<ICollection<Pokemon>> GetPokemonsByOwnerAsync(int ownerId);

    }
}