namespace PokemonReviewApp.Models
{
    public class User
    {
        public int id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

    }
}
