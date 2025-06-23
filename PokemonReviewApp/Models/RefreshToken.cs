namespace PokemonReviewApp.Models
{
    public class RefreshToken:BaseDomainModel
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
        public bool IsUsed { get; set; } = false;
        public bool IsRevoked { get; set; } = false;

        public bool IsExpired => DateTime.UtcNow >= Expires;
        public bool IsActive => !IsUsed && !IsRevoked && !IsExpired;

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
