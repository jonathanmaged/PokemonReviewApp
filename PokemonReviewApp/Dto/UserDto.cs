using System.ComponentModel.DataAnnotations;

namespace PokemonReviewApp.Dto
{
    public class UserDto
    {
        [Required]
        public string UserName { get; set; } 
        [Required]
        public string Password { get; set; } 
    }
}
