using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces.Repository;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
    public class OwnerRepository : GenericRepository<Owner>,IOwnerRepository
    {
        private readonly DataContext _context;
        public OwnerRepository(DataContext context):base(context) 
        {
            _context = context;
        }

        public async Task<Owner?> GetOwnerByNameAsync(string lastname)
        {
            return await _context.Owners.FirstOrDefaultAsync(o => o.LastName == lastname);

        }
        public async Task<ICollection<Owner>> GetOwnersOfAPokemonAsync(int pokeId)
        {
            var owners = await _context.PokemonsOwners.Where(po => po.PokemonId == pokeId)
                .Select(po => po.Owner)
                .ToListAsync();
            return owners;
        }
        public async Task<ICollection<Pokemon>> GetPokemonsByOwnerAsync(int ownerId)
        {
            var pokemons = await _context.PokemonsOwners.Where(po => po.OwnerId == ownerId)
                .Select(po => po.Pokemon)
                .ToListAsync();
            return pokemons;
        }

    }
}