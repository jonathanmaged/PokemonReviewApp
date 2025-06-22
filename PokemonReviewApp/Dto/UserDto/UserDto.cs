using System.ComponentModel.DataAnnotations;

namespace PokemonReviewApp.Dto.UserDto
{
    public class UserDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty ;
        [Required]
        public string Password { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
    }
}
