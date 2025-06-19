namespace PokemonReviewApp.Models
{
    public class User : BaseDomainModel
    {
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string? RefreshToken { get; set; } 
        public DateTime? RefreshTokenExpiryDate { get; set; } 

    }
}
