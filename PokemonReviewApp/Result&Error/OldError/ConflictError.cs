namespace PokemonReviewApp.Errors
{
    public class ConflictError<T>:Error where T:class
    {
        public T Entity { get; }

        public ConflictError(string message, T entity )
        {
            Message = message;
            Entity = entity;
        }
    }
}
