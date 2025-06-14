using System.ComponentModel.DataAnnotations;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Dto.GetDto
{
    public class PokemonDto
    {
        [Required]
        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        [Required]
        public int? CategoryId { get; set; }


    }
}
