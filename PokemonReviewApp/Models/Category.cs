namespace PokemonReviewApp.Models;

public class Category:BaseDomainModel
{
    public string Name { get; set; }
    public ICollection<Pokemon>? Pokemons { get; set; } = new List<Pokemon>() {  };
}

