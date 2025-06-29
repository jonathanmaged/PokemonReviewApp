using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Data
{
    public class DataContext: IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
               
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<PokemonOwner> PokemonsOwners { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PokemonOwner>().
                HasKey(po => new { po.OwnerId , po.PokemonId });

            modelBuilder.Entity<PokemonOwner>()
                .HasOne(po => po.Pokemon)
                .WithMany(p => p.PokemonOwners)
                .HasForeignKey(po => po.PokemonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PokemonOwner>()
                .HasOne(po => po.Owner)
                .WithMany(o => o.OwnerPokemons )
                .HasForeignKey(po => po.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Owner>()
                .HasOne(o => o.Country)
                .WithMany(c => c.Owners)
                .HasForeignKey(o => o.CountryId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Pokemon>()
            .HasOne(o => o.Category)
            .WithMany(c => c.Pokemons)
            .HasForeignKey(o => o.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<RefreshToken>()
                .Property(rt => rt.IsActive)
                .HasComputedColumnSql(
                "CAST(Case When [IsUsed] = 0 And [IsRevoked] = 0 And [Expires] > GETUTCDATE() THEN 1 ELSE 0 END AS bit)"
                , stored: false);

            // check on entity state and handling created at and modified 
            base.OnModelCreating(modelBuilder);
        }
    }
}
