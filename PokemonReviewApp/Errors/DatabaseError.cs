namespace PokemonReviewApp.Errors
{
    public class DatabaseError:Error
    {
        public DatabaseError(string message)
        {
            Message = message;
        }

    }
}
