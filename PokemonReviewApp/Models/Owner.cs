namespace PokemonReviewApp.Models;

public class Owner : BaseDomainModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gym { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; }
    public ICollection<PokemonOwner>? OwnerPokemons { get; set; } = new List<PokemonOwner>();
}

