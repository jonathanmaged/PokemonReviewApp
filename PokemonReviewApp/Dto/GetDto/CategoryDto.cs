using System.ComponentModel.DataAnnotations;

namespace PokemonReviewApp.Dto.GetDto
{
    public class CategoryDto
    {
        [Required]
        public int? Id { get; set; }
        public string Name { get; set; }
    }
}
