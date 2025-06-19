namespace PokemonReviewApp.Models;

public class Country : BaseDomainModel
{
    public string Name { get; set; }
    public ICollection<Owner>? Owners { get; set; } = new List<Owner>() { };
}

