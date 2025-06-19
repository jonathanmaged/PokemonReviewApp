namespace PokemonReviewApp.Models
{
    public abstract class BaseDomainModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
