using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces.Repository;

namespace PokemonReviewApp.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext context;

        public GenericRepository(DataContext dataContext)
        {
            this.context = dataContext;
        }
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync();
            
        }

        public virtual async Task<T> GetById(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }
        public virtual void  Add(T entity)
        {
             context.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }


        public virtual void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }


    }
}
