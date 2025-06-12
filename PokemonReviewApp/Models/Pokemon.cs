namespace PokemonReviewApp.Models;

public class Pokemon
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public int CategoryId { get; set; }
    public Category  Category { get; set; }
    public ICollection<PokemonOwner> PokemonOwners { get; set; }
    public ICollection<Review> Reviews { get; set; }

}

