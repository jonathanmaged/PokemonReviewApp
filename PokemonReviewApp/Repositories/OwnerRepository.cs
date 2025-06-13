using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces.Repository;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
    public class OwnerRepository : GenericRepository<Owner>,IOwnerRepository
    {
        private readonly DataContext context;
        public OwnerRepository(DataContext context):base(context) 
        {
            this.context = context;
        }

        public async Task<Owner?> GetOwnerByNameAsync(string lastname)
        {
            return await context.Owners.FirstOrDefaultAsync(o => o.LastName.Trim().ToLower() == lastname.Trim().ToLower());

        }
        public async Task<ICollection<Owner>> GetOwnersOfAPokemonAsync(int pokeId)
        {
            var owners = await context.PokemonsOwners.Where(po => po.PokemonId == pokeId)
                .Select(po => po.Owner)
                .ToListAsync();
            return owners;
        }
        public async Task<ICollection<Pokemon>> GetPokemonsByOwnerAsync(int ownerId)
        {
            var pokemons = await context.PokemonsOwners.Where(po => po.OwnerId == ownerId)
                .Select(po => po.Pokemon)
                .ToListAsync();
            return pokemons;
        }

    }
}