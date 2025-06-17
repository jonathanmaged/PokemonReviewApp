using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces.Repository;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DataContext context;

        public UserRepository(DataContext context):base(context) 
        {
            this.context = context;
        }

        public async Task<User?> GetUserByUserNameAsNoTracking(string username)
        {
            return await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<bool> IsUserExistAsync(string username)
        {
            return await context.Users.AnyAsync(u => u.UserName == username);
        }
    }
}
