namespace PokemonReviewApp.Models;

public class Owner
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string Gym { get; set; }
    public ICollection<PokemonOwner> OwnerPockemons { get; set; }
    //owner to country many to one relationship
    public int CountryId { get; set; }
    public Country Country { get; set; }
}

