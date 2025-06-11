namespace PokemonReviewApp.Models;

public class Pokemon
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }

    //(pokemon - owner) many to many relationship
    public ICollection<PokemonOwner> PokemonOwners { get; set; }
    //(pokemon - Review) many to one relationship 
    public ICollection<Review> Reviews { get; set; }
    //(pokemon - category) many to one relationship
    public int CategoryId { get; set; }
    public Category Category { get; set; }

}

