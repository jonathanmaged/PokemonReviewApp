namespace PokemonReviewApp.Models;

public class Owner
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Gym { get; set; }
    ICollection<PokemonOwner> OwnerPockemons { get; set; }
    //owner to country many to one relationship
    public Country Country { get; set; }
}

