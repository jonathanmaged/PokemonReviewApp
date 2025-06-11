namespace PokemonReviewApp.Errors
{
    public class ConflictError:Error
    {
        public ConflictError(string message)
        {
            Message = message;
        }
    }
}
