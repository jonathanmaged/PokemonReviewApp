namespace PokemonReviewApp.Models;

public class Review
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }

    public Pokemon Pokemon { get; set; }

    //(review - reviewer) many to one relationship
    public Reviewer Reviewer { get; set; }

}

