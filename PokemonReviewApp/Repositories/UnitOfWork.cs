using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces.Repository;

namespace PokemonReviewApp.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly DataContext context;
        public ICategoryRepository CategoryRepository { get; }
        public ICountryRepository CountryRepository { get; }
        public IOwnerRepository OwnerRepository { get; }
        public IPokemonRepository PokemonRepository { get; }

        public UnitOfWork(DataContext context,ICategoryRepository categoryRepository,
               ICountryRepository countryRepository,IOwnerRepository ownerRepository,
               IPokemonRepository pokemonRepository)
        {
            this.context = context;
            CategoryRepository = categoryRepository;
            CountryRepository = countryRepository;
            OwnerRepository = ownerRepository;
            PokemonRepository = pokemonRepository;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }

        public async Task<int> Save()
        {
            return await context.SaveChangesAsync();
        }
    }
}
