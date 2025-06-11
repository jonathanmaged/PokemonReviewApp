using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces.Repository;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _context;

        public OwnerRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Owner>> GetOwnersAsync()
        {
            return await _context.Owners.ToListAsync();
        }

        public async Task<Owner?> GetOwnerByIdAsync(int ownerId)
        {
            return await _context.Owners.FirstOrDefaultAsync(c => c.Id == ownerId);
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

        public async Task<bool> OwnerExistsAsync(int ownerId)
        {
            return await _context.Owners.AnyAsync(o => o.Id == ownerId);
        }

        public async Task<bool> CreateOwnerAsync(Owner owner)
        {
            _context.Owners.Add(owner);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0 ? true : false;

        }
    }
}