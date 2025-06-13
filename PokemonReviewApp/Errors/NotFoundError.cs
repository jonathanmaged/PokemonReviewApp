namespace PokemonReviewApp.Errors
{
    public class NotFoundError:Error
    {
        public NotFoundError(string message)
        {
            Message = message;
        }
    
    }
}
