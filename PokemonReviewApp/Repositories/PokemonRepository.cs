using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces.Repository;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
    public class PokemonRepository :GenericRepository<Pokemon>,IPokemonRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public PokemonRepository(DataContext context,IMapper mapper):base(context)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Pokemon?> GetPokemonByNameAsync(string name)
        {
            return await context.Pokemons.FirstOrDefaultAsync(p => p.Name == name);

        }
        public async Task<double> GetPokemonRatingAsync(int id)
        {
            var reviews = context.Reviews.Where(r => r.PokemonId == id);
            if (await reviews.CountAsync() == 0)
                return 0;
            return await reviews.AverageAsync(r => r.Rating);
        }
    }
}