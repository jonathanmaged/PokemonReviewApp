using PokemonReviewApp.Models;

namespace PokemonReviewApp.Dto
{
    public class PokemonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string CategoryName { get; set; }
        public ICollection<SimpleOwnerDto> Owners { get; set; }
        public ICollection<ReviewDto> Reviews { get; set; }
    }
}
