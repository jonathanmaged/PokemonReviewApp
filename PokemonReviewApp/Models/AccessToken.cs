namespace PokemonReviewApp.Models
{
    public class AccessToken
    {
        Guid Id { get; set; }
        public bool IsRevoked { get; set; }
    }
}
