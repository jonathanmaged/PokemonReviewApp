namespace PokemonReviewApp.Models;

public class Pokemon : BaseDomainModel
{
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public int CategoryId { get; set; }
    public Category  Category { get; set; }
    public ICollection<PokemonOwner>? PokemonOwners { get; set; } = new List<PokemonOwner>();
    public ICollection<Review>? Reviews { get; set; } = new List<Review>();

}

