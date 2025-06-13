namespace PokemonReviewApp.Dto.CreateDto
{
    public class CreateOwnerDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gym { get; set; }
        public IEnumerable<int> PokemonsId { get; set; }
    }
}
