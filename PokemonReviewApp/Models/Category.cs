namespace PokemonReviewApp.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }

    ICollection<Pokemon> Pokemons { get; set; }
}

