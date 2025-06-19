namespace PokemonReviewApp.Models;

public class Reviewer : BaseDomainModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    ICollection<Review> Reviews { get; set; }
}

